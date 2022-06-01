use std::fmt::{Display, Formatter};

#[derive(PartialEq, Eq, Hash, Copy, Clone, Debug)]
pub enum Bool {
    True,
    False,
}

impl std::ops::BitAnd for Bool {
    type Output = Self;

    fn bitand(self, rhs: Self) -> Self::Output {
        if self == Bool::True { rhs } else { Bool::False }
    }
}

impl std::ops::BitOr for Bool {
    type Output = Self;

    fn bitor(self, rhs: Self) -> Self::Output {
        if self == Bool::False { rhs } else { Bool::True }
    }
}

impl std::ops::Not for Bool {
    type Output = Self;

    fn not(self) -> Self::Output {
        match self {
            Bool::True => { Bool::False }
            Bool::False => { Bool::True }
        }
    }
}

impl Display for Bool {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        let value = match self {
            Bool::True => "true",
            Bool::False => "false",
        };
        write!(f, "{}", value)
    }
}
