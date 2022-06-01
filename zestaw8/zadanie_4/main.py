use std::rc::Weak;

// region Actions
static mut COUNTER: usize = 0;


pub enum UserInput {
    ShutDown,
    BeginMaintenance,
    CardInserted,
}

impl UserInput {
    pub fn get_some() -> Self {
        match unsafe { COUNTER.overflowing_add(1).0 } % 3 {
            0 => Self::ShutDown,
            1 => Self::BeginMaintenance,
            2 => Self::CardInserted,
            _ => panic!("number out of range")
        }
    }
}

pub enum NormalActionOutcome {
    Success,
    Failure,
}

impl NormalActionOutcome {
    pub fn get_some() -> Self {
        match unsafe { COUNTER.overflowing_add(1).0 & 1 == 1 } {
            true => Self::Success,
            false => Self::Failure,
        }
    }
}

pub enum ServiceCustomer {
    NormalActionOutcome(NormalActionOutcome),
    Cancel,
}

impl ServiceCustomer {
    pub fn get_some() -> Self {
        match unsafe { COUNTER.overflowing_add(1).0 } % 3 {
            0 => Self::NormalActionOutcome(NormalActionOutcome::Success),
            1 => Self::NormalActionOutcome(NormalActionOutcome::Failure),
            2 => Self::Cancel,
            _ => panic!("number out of range")
        }
    }
}
// endregion


pub struct Atm {
    pub state: Option<Box<dyn IState>>
}


impl Atm {
    pub fn unpowered() -> Self {
        let mut instance = Self { state: None };
        instance.state = Some(Box::new(OffState(&mut instance)));
        instance
    }

    pub fn shut_down(&mut self) { self.state.unwrap().on_shut_down().unwrap(); }

    pub fn start_up(&mut self) { self.state.unwrap().on_start_up().unwrap(); }

    pub fn self_test(&mut self) {
        match NormalActionOutcome::get_some() {
            NormalActionOutcome::Success => { self.state.unwrap().on_self_test_success() }
            NormalActionOutcome::Failure => { self.state.unwrap().on_self_test_failure() }
        }.unwrap();
    }

    pub fn maintenance(&mut self) { 
        match NormalActionOutcome::get_some() {
            NormalActionOutcome::Success => { self.state.unwrap().on_maintenance_success() }
            NormalActionOutcome::Failure => { self.state.unwrap().on_maintenance_failure() }
        }.unwrap();
    }
    
    pub fn process_user_input(&mut self) { 
        match UserInput::get_some() {
            UserInput::ShutDown => self.state.unwrap().on_shut_down(),
            UserInput::BeginMaintenance => self.state.unwrap().on_begin_maintenance(),
            UserInput::CardInserted => self.state.unwrap().on_card_inserted(),
        }.unwrap();
    }
    
    pub fn out_of_service_repair(&mut self) {

    }


    pub fn set_state(&mut self, new_state: Box<dyn IState>) {
        self.state = new_state;
    }

    pub fn start_maintenance(&mut self) {

    }
}


#[derive(Debug)]
pub struct InvalidOperationError;


/// Default implementation returns error, states can override methods in which they are interested.
pub trait IState {
    fn message(&self) -> String;
    fn on_shut_down(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_start_up(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_self_test_success(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_self_test_failure(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_begin_maintenance(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_maintenance_success(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_maintenance_failure(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_customer_service_cancel(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_customer_service_success(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_customer_service_failure(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_out_of_order_service(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
    fn on_card_inserted(&mut self) -> Result<(), InvalidOperationError> { Err(InvalidOperationError) }
}


// region OffState
pub struct OffState(pub Weak<Atm>);

impl IState for OffState {
    fn message(&self) -> String {
        format!("")
    }

    fn on_start_up(&mut self) -> Result<(), InvalidOperationError> {
        self.A
    }
}
// endregion


// region SelfTestState
pub struct SelfTestState(Weak<Atm>);

impl IState for SelfTestState {
    fn message(&self) -> String {
        format!("Running self test...")
    }

    fn on_self_test_success(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new());
        Ok(())
    }

    fn on_self_test_failure(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(OutOfServiceState(self.0)));
        Ok(())
    }
}
// endregion


// region IdleState
pub struct IdleState(Weak<Atm>);

impl IState for IdleState {
    fn message(&self) -> String {
        format!("Insert card to begin.")
    }

    fn on_shut_down(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(OffState(self.0)));
        Ok(())
    }

    fn on_begin_maintenance(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(MaintenanceState(self.0)));
        Ok(())
    }

    fn on_card_inserted(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(ServingCustomerState(self.0)));
        Ok(())
    }
}
// endregion


// region MaintenanceState
pub struct MaintenanceState(Weak<Atm>);

impl IState for MaintenanceState {
    fn message(&self) -> String {
        format!("Some diagnostic information.")
    }

    fn on_maintenance_success(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(SelfTestState(self.0)));
        Ok(())
    }

    fn on_maintenance_failure(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(OutOfServiceState(self.0)));
        Ok(())
    }
}
// endregion


// region ServingCustomerState
pub struct ServingCustomerState(Weak<Atm>);

impl IState for ServingCustomerState {
    fn message(&self) -> String {
        format!("Instructions for customer")
    }

    fn on_customer_service_cancel(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(IdleState(self.0)));
        Ok(())
    }

    fn on_customer_service_success(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(IdleState(self.0)));
        Ok(())
    }

    fn on_customer_service_failure(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(OutOfServiceState(self.0)));
        Ok(())
    }
}
// endregion


// region OutOfServiceState
pub struct OutOfServiceState(Weak<Atm>);

impl IState for OutOfServiceState {
    fn message(&self) -> String {
        format!("Out of service...")
    }

    fn on_shut_down(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(OffState(self.0)));
        Ok(())
    }

    fn on_out_of_order_service(&mut self) -> Result<(), InvalidOperationError> {
        self.0.set_state(Box::new(MaintenanceState(self.0)));
        Ok(())
    }
}
// endregion


fn main() {
    println!("Hello, world!");
}
