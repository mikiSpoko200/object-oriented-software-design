mod context;
mod expression;
mod value;

use expression::{
    Expression,
    VariableExpression,
    LogicalNegationExpression,
    LogicalAndExpression,
    LogicalOrExpression,
    LiteralExpression,
};
use context::{Environment, Context};
use value::Bool;

fn main() -> Result<(), Box<dyn std::error::Error>>{
    let mut env = Environment::default();

    // Create few variables
    let x = VariableExpression::new(String::from("x"));
    let y = VariableExpression::new(String::from("y"));
    let z = VariableExpression::new(String::from("z"));

    // Set variable's values
    env.set_value(x.clone(), Bool::True)?;
    env.set_value(y.clone(), Bool::True)?;
    env.set_value(z.clone(), Bool::False)?;

    // Create an expression
    let expr = LogicalAndExpression::new(
        Box::new(x.clone()),
        Box::new(LogicalOrExpression::new(
            Box::new(LogicalNegationExpression::new(Box::new(y.clone()))),
            Box::new(
                LogicalAndExpression::new(
                    Box::new(LiteralExpression::new(Bool::True)),
                    Box::new(z.clone())
            ))
        ))
    );
    println!("x: {}", x.interpret(&env)?);
    println!("y: {}", y.interpret(&env)?);
    println!("z: {}", z.interpret(&env)?);

    let w = VariableExpression::new("w".into());
    println!("Example of evaluation error:\n> {}", w.interpret(&env).err().unwrap());
    println!("{}", expr);
    println!("{}", expr.interpret(&env)?);
    Ok(())
}


#[cfg(test)]
mod tests {
    use crate::expression::{
        Expression,
        VariableExpression,
        LogicalNegationExpression,
        LogicalAndExpression,
        LogicalOrExpression,
        LiteralExpression,
    };
    use crate::context::{Environment, Context};
    use crate::value::Bool;

    #[test]
    fn test_literal_false() {
        let env = Environment::default();
        let literal = LiteralExpression::new(Bool::False);
        assert_eq!(literal.interpret(&env).unwrap(), Bool::False);
    }

    #[test]
    fn test_literal_true() {
        let env = Environment::default();
        let literal = LiteralExpression::new(Bool::True);
        assert_eq!(literal.interpret(&env).unwrap(), Bool::True);
    }

    #[test]
    fn test_env_get_variable_valid() {
        let mut env = Environment::default();
        let x = VariableExpression::new("x".into());
        env.set_value(x.clone(), Bool::True).unwrap();
        assert_eq!(x.interpret(&env).unwrap(), Bool::True);
    }

    #[test]
    fn test_env_get_variable_invalid() {
        let mut env = Environment::default();
        let x = VariableExpression::new("x".into());
        let y = VariableExpression::new("y".into());
        env.set_value(x.clone(), Bool::True).unwrap();

        let result = y.interpret(&env);
        assert!(result.is_err());
    }

    #[test]
    fn test_negation() {
        let env = Environment::default();
        let result = LogicalNegationExpression::new(
            Box::new(LiteralExpression::new(Bool::True))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::False);
    }

    // region Logical And tests

    #[test]
    fn test_and_1() {
        let env = Environment::default();
        let result = LogicalAndExpression::new(
            Box::new(LiteralExpression::new(Bool::True)),
            Box::new(LiteralExpression::new(Bool::True))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::True);
    }

    #[test]
    fn test_and_2() {
        let env = Environment::default();
        let result = LogicalAndExpression::new(
            Box::new(LiteralExpression::new(Bool::True)),
            Box::new(LiteralExpression::new(Bool::False))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::False);
    }

    #[test]
    fn test_and_3() {
        let env = Environment::default();
        let result = LogicalAndExpression::new(
            Box::new(LiteralExpression::new(Bool::False)),
            Box::new(LiteralExpression::new(Bool::True))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::False);
    }

    #[test]
    fn test_and_4() {
        let env = Environment::default();
        let result = LogicalAndExpression::new(
            Box::new(LiteralExpression::new(Bool::False)),
            Box::new(LiteralExpression::new(Bool::False))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::False);
    }

    // endregion

    // region Logical Or tests

    #[test]
    fn test_or_1() {
        let env = Environment::default();
        let result = LogicalOrExpression::new(
            Box::new(LiteralExpression::new(Bool::True)),
            Box::new(LiteralExpression::new(Bool::True))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::True);
    }

    #[test]
    fn test_or_2() {
        let env = Environment::default();
        let result = LogicalOrExpression::new(
            Box::new(LiteralExpression::new(Bool::True)),
            Box::new(LiteralExpression::new(Bool::False))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::True);
    }

    #[test]
    fn test_or_3() {
        let env = Environment::default();
        let result = LogicalOrExpression::new(
            Box::new(LiteralExpression::new(Bool::False)),
            Box::new(LiteralExpression::new(Bool::True))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::True);
    }

    #[test]
    fn test_or_4() {
        let env = Environment::default();
        let result = LogicalOrExpression::new(
            Box::new(LiteralExpression::new(Bool::False)),
            Box::new(LiteralExpression::new(Bool::False))
        ).interpret(&env).unwrap();
        assert_eq!(result, Bool::False);
    }
    // endregion

}