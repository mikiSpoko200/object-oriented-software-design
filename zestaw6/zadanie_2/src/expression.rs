use std::error::Error;
use std::fmt::{Display, Formatter};
use crate::{context::{Environment, Context}, value::Bool};

/// Expression trait.
///
/// Expression should be convertable to String with nice formatting thus Display trait bound.
/// Expression evaluation with interpret method can also fail with some generic error
/// be it syntax error, undefined symbol error and so on.
pub trait Expression : Display {
    fn interpret(&self, _: &Environment) -> Result<Bool, Box<dyn Error>>;
}

// region Literal

#[derive(Clone)]
pub struct LiteralExpression {
    value: Bool
}

impl LiteralExpression {
    pub const fn new(value: Bool) -> Self {
        Self { value }
    }
}

impl Display for LiteralExpression {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        write!(f, "{}", self.value)
    }
}

impl Expression for LiteralExpression {
    fn interpret(&self, _: &Environment) -> Result<Bool, Box<dyn Error>> {
        Ok(self.value)
    }
}
// endregion

// region Variable

#[derive(Hash, PartialEq, Eq, Debug, Clone)]
pub struct VariableExpression {
    ident: String,
}

impl VariableExpression {
    pub const fn new(ident: String) -> Self {
        Self { ident }
    }
}

impl Display for VariableExpression {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        write!(f, "{}", self.ident)
    }
}

impl Expression for VariableExpression {
    fn interpret(&self, env: &Environment) -> Result<Bool, Box<dyn Error>> {
        env.get_value(self.clone())
    }
}

// endregion

// region Logical And Expression

pub struct LogicalAndExpression {
    expr_lhs: Box<dyn Expression>,
    expr_rhs: Box<dyn Expression>,
}

impl LogicalAndExpression {
    pub fn new(expr_lhs: Box<dyn Expression>, expr_rhs: Box<dyn Expression>) -> Self {
        Self { expr_lhs, expr_rhs }
    }
}

impl Display for LogicalAndExpression {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        write!(f, "({} & {})", self.expr_lhs, self.expr_rhs)
    }
}

impl Expression for LogicalAndExpression {
    fn interpret(&self, env: &Environment) -> Result<Bool, Box<dyn Error>> {
        Ok(*&self.expr_lhs.interpret(env)? & *&self.expr_rhs.interpret(env)?)
    }
}

// endregion

// region Logical Or Expression

pub struct LogicalOrExpression {
    expr_lhs: Box<dyn Expression>,
    expr_rhs: Box<dyn Expression>,
}

impl LogicalOrExpression {
    pub  fn new(expr_lhs: Box<dyn Expression>, expr_rhs: Box<dyn Expression>) -> Self {
        Self { expr_lhs, expr_rhs }
    }
}

impl Display for LogicalOrExpression {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        write!(f, "({} | {})", self.expr_lhs, self.expr_rhs)
    }
}

impl Expression for LogicalOrExpression {
    fn interpret(&self, env: &Environment) -> Result<Bool, Box<dyn Error>> {
        Ok(*&self.expr_lhs.interpret(env)? | *&self.expr_rhs.interpret(env)?)
    }
}

// endregion

// region Unary Expression

pub struct LogicalNegationExpression {
    expr: Box<dyn Expression>,
}

impl LogicalNegationExpression {
    pub  fn new(expr: Box<dyn Expression>) -> Self {
        Self { expr }
    }
}

impl Display for LogicalNegationExpression {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        write!(f, "~{}", self.expr)
    }
}

impl Expression for LogicalNegationExpression {
    fn interpret(&self, env: &Environment) -> Result<Bool, Box<dyn Error>> {
        Ok(!self.expr.interpret(env)?)
    }
}

// endregion
