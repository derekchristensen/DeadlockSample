# System.CommandLine Deadlock Sample

- [Related GitHub issue](https://github.com/dotnet/command-line-api/issues/832)

System.CommandLine v2.0.0-beta1.21216.1

This project demonstrates a deadlock condition in the System.CommandLine library.
Based on my debugging the deadlock happens when a command handler is returning
and the Ctrl+C handler is trying to detach the event handler on the
`Console.CancelKeyPress` event handler. It appears to lock the thread when trying
to detach the handler. The issue seems to be dependent on an assembly being loaded
via `Assembly.LoadFile`.

Stack traces of the deadlocked state:

![Worker Thread Callstack](https://user-images.githubusercontent.com/75036159/120870654-b5082000-c556-11eb-92da-83b03b42bd4e.png)

![Main Thread Callstack](https://user-images.githubusercontent.com/75036159/120870686-c3563c00-c556-11eb-8458-e19278b025d2.png)
