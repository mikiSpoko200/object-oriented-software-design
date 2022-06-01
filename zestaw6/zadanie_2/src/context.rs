use std::collections::hash_map::Entry;
use std::collections::HashMap;
use std::fmt::{Debug, Formatter};
use crate::{
    expression::VariableExpression,
    value::Bool
};


///
/// ## Why generic over result Error type?
///
/// There are multiple possible error cases that can arise during variable assigment / access.
/// In more general context of some programming language that is.
/// There can be type mismatch, variable can be absent to name a few.
/// What would we like to return in that case? Some concrete ContextError? (Actually maybe?).
/// But one could argue that we should provide client the ability to return specific VariableLookupError
/// and TypeMismatchError. Currently there is no way to achieve this without generics
/// and for some reason that wouldn't seat right with me.
///
pub trait Context {
    fn get_value(&self, _: VariableExpression) -> Result<Bool, Box<dyn std::error::Error>>;

    fn set_value(&mut self, _: VariableExpression, _: Bool) -> Result<(), Box<dyn std::error::Error>>;
}


/// Concrete implementation of interpretation context.
pub struct Environment {
    variable_lookup: HashMap<VariableExpression, Bool>
}

impl Default for Environment {
    fn default() -> Self {
        Self { variable_lookup: HashMap::new() }
    }
}

impl Context for Environment {
    fn get_value(&self, var: VariableExpression) -> Result<Bool, Box<dyn std::error::Error>> {
        match self.variable_lookup.get(&var) {
            None => Err(Box::new(VariableLookupError::new(var))),
            Some(&value) => Ok(value),
        }
    }

    fn set_value(&mut self, var: VariableExpression, val: Bool) -> Result<(), Box<dyn std::error::Error>> {
        match self.variable_lookup.entry(var) {
            Entry::Occupied(mut o) => {o.insert(val); }
            Entry::Vacant(o) => { o.insert(val); }
        };
        Ok(())
    }
}


/// Variable lookup error.
/// returned when variable expression evaluation fails due to
/// it missing from the environment.
#[derive(Debug)]
pub struct VariableLookupError {
    var: VariableExpression,
}

impl VariableLookupError {
    pub const fn new(var: VariableExpression) -> Self {
        Self { var }
    }
}

impl std::fmt::Display for VariableLookupError {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        write!(f, "undefined variable: {}", self.var)
    }
}

impl std::error::Error for VariableLookupError {}
