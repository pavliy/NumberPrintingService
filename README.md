# Numbers printing coding task

REST API accepts number and pushes it to TCP server. Server should parse that number and display in console.
It could be UI to show instead of console, but the timing is restricted as 2 hours, so this is as is. 

## Getting Started

There are 2 projects actually:
1. PrintAPI
2. PrintServer

You can simply run them both and play with swagger.
Of course, it can be all improved, dockerized etc. But again - there is a strong limit in time.

Overall what you'll be able to find there:
- Resharper and stylecop rules - this is to keep codebase well-formed and clean.
- Logging is covered by NLog. I'm big fan of its fluent syntaxis.
- TcpClient is well abstracted so tests can be written for web api layer easily. I do regularly use nunit + fluent assertions for that.

## General follow up
There are some "lacks" in current implementation and I'd like to point them out:
- There is no processor for errors. It should be exception filter in place. And we should get fancy HTTP Codes to correctly indicate what happened.
- There is no validation for passed number. In my daily work I do actively use FluentValidation library + Decorator pattern to handle this in fancy manner. But I decided that this can be omitted for now.
- Print server for sure can be improved. Right now it's really "very basic". Ideally it should communicate about errors and client should process them as well.
- There are no retry policies in place. But they also can be added
- Talking about healthchecks - asp.net core out of the box gives very easy "plan" how to implement healthchecks. I did that on multiple daily projects in .net core 3.1, but need just more time here.

## Authors

Eugene P.

## p.s.
Though, I'm out of time - I'll add some extra stuff a bit later. 
And again, there is always a room for improvements :)