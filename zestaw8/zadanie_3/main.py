import sqlite3
from abc import ABC, abstractmethod
from io import TextIOWrapper
from pathlib import Path
from typing import Optional
from xml.etree import ElementTree


class IDataAccessHandlerStrategy(ABC):
    @abstractmethod
    def open_connection(self) -> None:
        pass

    @abstractmethod
    def close_connection(self) -> None:
        pass

    @abstractmethod
    def load_data(self) -> None:
        pass

    @abstractmethod
    def process_data(self) -> None:
        pass


class SqlLiteDataAccessHandlerStrategy(IDataAccessHandlerStrategy):
    def __init__(self, /, db_path: Path) -> None:
        self.__db_path = db_path
        self.__numbers = list()
        self.__connection = None

    def open_connection(self) -> None:
        self.__connection = sqlite3.connect(self.__db_path.name)

    def close_connection(self) -> None:
        self.__connection.close()

    def load_data(self) -> None:
        cursor = self.__connection.cursor()
        self.__numbers = [int(number) for (number,) in cursor.execute("select number from numbers")]

    def process_data(self) -> None:
        print(f"Sum of all fields in database column: {sum(self.__numbers)}")


class XmlDataAccessHandlerStrategy(IDataAccessHandlerStrategy):
    def __init__(self, file_path: Path, encoding: str = "utf8") -> None:
        self.encoding = encoding
        self.__file_path = file_path
        self.__xml_tree: Optional[ElementTree] = None
        self.__file_handle: Optional[TextIOWrapper] = None

    def open_connection(self) -> None:
        self.__file_handle = open(self.__file_path, "r", encoding=self.encoding)

    def close_connection(self) -> None:
        self.__file_handle.close()

    def load_data(self) -> None:
        self.__xml_tree = ElementTree.fromstring(self.__file_handle.read())

    def process_data(self) -> None:
        node_with_longest_tag = max(self.__xml_tree, key=lambda node: len(node.tag))
        print(f"Xml tree node with longest tag: {node_with_longest_tag.tag}")


class DataAccessHandler:
    def __init__(self, strategy: IDataAccessHandlerStrategy) -> None:
        self.strategy = strategy

    def execute(self) -> None:
        self.strategy.open_connection()
        self.strategy.load_data()
        self.strategy.process_data()
        self.strategy.close_connection()


def main():
    database_connection_strategy = SqlLiteDataAccessHandlerStrategy(Path.cwd() / "example.db")
    xml_connection_strategy = XmlDataAccessHandlerStrategy(Path.cwd() / "example.xml")

    data_access_handler = DataAccessHandler(database_connection_strategy)
    data_access_handler.execute()

    data_access_handler.strategy = xml_connection_strategy
    data_access_handler.execute()


if __name__ == '__main__':
    main()
