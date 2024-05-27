# RavenDB Testing

This project contains examples on how to write Unit Tests for testing business logic which stores or retrieves data from RavenDB database.

Tests are using an official `RavenTestDriver` which runs an in-memory instance of RavenDB for tests run not through mockup but through real database.
This helps reproduce potential realistic exceptions and other failures in tests.

This project demonstrates that it can be quite easy to setup and do the tests with the real database with code only in tests without any external database setup.
