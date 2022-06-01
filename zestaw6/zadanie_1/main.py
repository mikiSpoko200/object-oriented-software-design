# -*- encoding: utf-8 -*-
from abc import ABC, abstractmethod
from enum import Enum, auto
from typing import Optional


class LogType(Enum):
    """Enumeration of Logger work modes."""
    NONE = auto()
    CONSOLE = auto()
    FILE = auto()


class ILogger(ABC):
    """Logger interface."""
    @abstractmethod
    def log(self, message: str) -> None:
        """Log a new message."""
        pass


class LoggerFactory:
    """Logger factory."""

    __instance: Optional[ILogger] = None
    __logger_type: Optional[LogType] = None

    @staticmethod
    def get_logger(log_type: LogType, params: Optional[str] = None) -> ILogger:
        """Create new instance of ILogger with configuration specified."""
        if LoggerFactory.__instance is None or LoggerFactory.__logger_type is not log_type:
            LoggerFactory.__logger_type = log_type
            match log_type:
                case LogType.CONSOLE:
                    LoggerFactory.__instance = ConsoleLogger(params)
                case LogType.FILE:
                    LoggerFactory.__instance = FileLogger(params)
                case LogType.NONE:
                    LoggerFactory.__instance = NullLogger()
        return LoggerFactory.__instance


class ConsoleLogger(ILogger):
    """Console Logger."""
    def __init__(self, message_format: str = None):
        self.message_format = message_format

    def log(self, message: str) -> None:
        if self.message_format is None:
            print(message)
        else:
            print(self.message_format % message)


class FileLogger(ILogger):
    """File Logger."""
    def __init__(self, log_file: str) -> None:
        self.log_file = log_file

    def log(self, message: str) -> None:
        with open(self.log_file, 'a', encoding="utf-8") as file_handle:
            file_handle.write(message + '\n')


class NullLogger(ILogger):
    """Null Object Logger."""
    def log(self, message: str) -> None:
        pass


def main():
    logger = LoggerFactory.get_logger(LogType.CONSOLE, "Hello from custom logger: %s")
    logger.log("Some message")
    logger.log("Some other message")
    logger.log("And yet another one!")

    # here logger should change to file type.

    new_logger = LoggerFactory.get_logger(LogType.FILE, "example_log_file.txt")
    new_logger.log("Now logging to file!")
    new_logger.log("It should be working")
    new_logger.log("Cool!")

    # and here again we should obtain a new logger

    null_logger = LoggerFactory.get_logger(LogType.NONE)
    null_logger.log("No")
    null_logger.log("logging")
    null_logger.log("from")
    null_logger.log("now")
    null_logger.log("on")


if __name__ == '__main__':
    main()
