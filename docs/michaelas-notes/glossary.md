# Glossary

1. _Controller_
    ```
    A type of class that defines an API interface for your application. This is the logical entrypoint of your API and determines what happens when clients make certain calls.
    ```
2. _Entity_
    ```
    Simply, an entity class is a representation of a complex piece of data in your application. Usually, this means that they have more than 1 piece of data. Examples would be User (has an ID, Email, Username, etc) or a Business (has an ID, Name, Type, etc).

    The entities in this repo map directly back to the PostgreSQL database, so what's defined in those classes added to the database.
    ```
