
namespace KramarDev.Quiz.DAL.Database;

public static class DbInitializer
{
    // dotnet ef migrations add InitialCreate -o Database/Migrations

    // Add any initials here.
    public static void Initialize(QuizDbContext ctx)
    {
        bool shouldWeSubmit = false;
        Technology[] techArray = new Technology[7];

        if (!ctx.Technologies.Any())
        {
            techArray[0] = new Technology() { Name = "C#", Description = "Test your knowledge of C# fundamentals, OOP concepts, LINQ, and the .NET ecosystem.", IsActive = true, DurationInMinutes = 12, QuestionCount = 10, IconName = "csharp", Color = "#68217a" };
            techArray[1] = new Technology() { Name = "JavaScript", Description = "Test your knowledge of JavaScript fundamentals, ES6+ features, and browser APIs.", IsActive = true, DurationInMinutes = 8, QuestionCount = 12, IconName = "javascript", Color = "#f0db4f" };
            techArray[2] = new Technology() { Name = "C/C++", Description = "Test your knowledge of C/C++ fundamentals, memory management, pointers, object-oriented programming, and standard libraries.", IsActive = true, DurationInMinutes = 10, QuestionCount = 12, IconName = "C++", Color = "#aa00aa" };
            techArray[3] = new Technology() { Name = "SQL", Description = "Test your SQL skills with questions on queries, joins, indexes, and database design.", IsActive = true, DurationInMinutes = 10, QuestionCount = 10, IconName = "database", Color = "#336791" };
            techArray[4] = new Technology() { Name = ".NET", Description = "Explore your understanding of the .NET framework, ASP.NET Core, Entity Framework, and middleware.", IsActive = true, DurationInMinutes = 12, QuestionCount = 10, IconName = "dotnet", Color = "#512bd4" };
            techArray[5] = new Technology() { Name = "React", Description = "Challenge yourself on React concepts, hooks, state management, and best practices.", IsActive = true, DurationInMinutes = 12, QuestionCount = 12, IconName = "react", Color = "#61dafb" };
            techArray[6] = new Technology() { Name = "English Grammar", Description = "Test your understanding of English grammar rules, tenses, parts of speech, and sentence structure.", IsActive = true, DurationInMinutes = 12, QuestionCount = 20, IconName = "language", Color = "#e44d26" };

            ctx.Technologies.AddRange(techArray);
            shouldWeSubmit = true;
        }
        else
        {
            techArray = ctx.Technologies.Take(3).ToArray();
        }

        if (!ctx.Questions.Any())
        {
            var questions = new List<Question>()
            {
                // C# — techArray[0]

                new() {
                    Text = "What is the main benefit of using 'IEnumerable<T>' instead of a concrete collection type in method parameters?",
                    Answer1 = "It guarantees random access by index",
                    Answer2 = "It reduces coupling by depending on an abstraction",
                    Answer3 = "It always improves performance",
                    Answer4 = "It makes the collection thread-safe",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which statement about deferred execution in LINQ is correct?",
                    Answer1 = "The query always executes immediately when defined",
                    Answer2 = "The query executes only when its results are enumerated, for many LINQ operators",
                    Answer3 = "Deferred execution is available only for arrays",
                    Answer4 = "Deferred execution means the query runs on another thread",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "When should you prefer 'StringBuilder' over string concatenation in C#?",
                    Answer1 = "Only when working with exactly two strings",
                    Answer2 = "When building a string through many repeated modifications",
                    Answer3 = "Always, because it is always faster",
                    Answer4 = "Only inside async methods",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What is the main purpose of 'IDisposable'?",
                    Answer1 = "To support JSON serialization",
                    Answer2 = "To release unmanaged resources deterministically",
                    Answer3 = "To allow inheritance",
                    Answer4 = "To make an object immutable",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which statement about 'record' types in C# is most accurate?",
                    Answer1 = "They are primarily intended for value-based equality scenarios",
                    Answer2 = "They always allocate on the stack",
                    Answer3 = "They cannot contain methods",
                    Answer4 = "They replace all classes in modern C#",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Why can exposing 'List<T>' directly from a class be problematic?",
                    Answer1 = "Because List<T> cannot store reference types",
                    Answer2 = "Because callers may modify internal state unexpectedly",
                    Answer3 = "Because List<T> is slower than arrays in all cases",
                    Answer4 = "Because List<T> cannot be serialized",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What is the difference between overriding and overloading?",
                    Answer1 = "Overriding changes behavior in a derived class; overloading uses the same name with different signatures",
                    Answer2 = "Overriding is for constructors only; overloading is for fields only",
                    Answer3 = "They are two names for the same concept",
                    Answer4 = "Overloading requires inheritance; overriding does not",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Why is catching 'Exception' broadly often discouraged?",
                    Answer1 = "Because it prevents the CLR from compiling the code",
                    Answer2 = "Because it can hide important failures and make debugging harder",
                    Answer3 = "Because only ArgumentException can be caught in C#",
                    Answer4 = "Because broad catches automatically terminate the process",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which statement about 'async' and 'await' is correct?",
                    Answer1 = "Using async automatically creates a new thread",
                    Answer2 = "Await suspends the method until the awaited operation completes without necessarily blocking the thread",
                    Answer3 = "Async methods cannot return values",
                    Answer4 = "Await can only be used in console applications",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What is the main reason to use interfaces in application design?",
                    Answer1 = "To reduce readability",
                    Answer2 = "To enable abstraction, loose coupling, and easier testing",
                    Answer3 = "To avoid using classes",
                    Answer4 = "To improve CPU clock speed",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which keyword prevents a class from being inherited in C#?",
                    Answer1 = "static",
                    Answer2 = "private",
                    Answer3 = "sealed",
                    Answer4 = "readonly",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which keyword is used to explicitly implement an interface member in C#?",
                    Answer1 = "override",
                    Answer2 = "explicit",
                    Answer3 = "interface",
                    Answer4 = "No special keyword is required",
                    CorrectAnswerNumber = 4,
                    Difficulty = 3,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What is the default value of an int field in C#?",
                    Answer1 = "null",
                    Answer2 = "0",
                    Answer3 = "1",
                    Answer4 = "undefined",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which keyword is used to define a method that can be overridden in a derived class?",
                    Answer1 = "abstract",
                    Answer2 = "override",
                    Answer3 = "virtual",
                    Answer4 = "sealed",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What does the 'null-coalescing' operator '??' do in C#?",
                    Answer1 = "Throws an exception if the value is null",
                    Answer2 = "Returns the left operand if not null; otherwise the right operand",
                    Answer3 = "Checks whether two values are equal",
                    Answer4 = "Converts null to zero",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which collection is best suited for Last-In-First-Out behavior?",
                    Answer1 = "Queue",
                    Answer2 = "Dictionary",
                    Answer3 = "List",
                    Answer4 = "Stack",
                    CorrectAnswerNumber = 4,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What is method overloading in C#?",
                    Answer1 = "Creating methods with the same name but different parameter lists",
                    Answer2 = "Replacing a base class method in a derived class",
                    Answer3 = "Calling multiple methods at once",
                    Answer4 = "Using too many parameters in one method",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which statement about structs in C# is correct?",
                    Answer1 = "Structs are reference types",
                    Answer2 = "Structs cannot have constructors",
                    Answer3 = "Structs are value types",
                    Answer4 = "Structs support inheritance from classes",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What does 'ToString()' do by default when overridden in a class?",
                    Answer1 = "Converts the object to binary",
                    Answer2 = "Provides a string representation of the object",
                    Answer3 = "Disposes the object",
                    Answer4 = "Clones the object",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which keyword is used to define a member that belongs to the type itself rather than an instance?",
                    Answer1 = "const",
                    Answer2 = "this",
                    Answer3 = "shared",
                    Answer4 = "static",
                    CorrectAnswerNumber = 4,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which keyword is used to define a class in C#?",
                    Answer1 = "class",
                    Answer2 = "struct",
                    Answer3 = "define",
                    Answer4 = "object",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What does the 'var' keyword do in C#?",
                    Answer1 = "Declares a dynamic variable",
                    Answer2 = "Declares an implicitly typed variable",
                    Answer3 = "Declares a constant",
                    Answer4 = "Declares a static variable",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which collection type stores key-value pairs in C#?",
                    Answer1 = "List",
                    Answer2 = "Queue",
                    Answer3 = "Dictionary",
                    Answer4 = "Stack",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What is the base class for all types in C#?",
                    Answer1 = "System.Base",
                    Answer2 = "System.Object",
                    Answer3 = "System.Type",
                    Answer4 = "System.Root",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which LINQ method is used to filter a sequence?",
                    Answer1 = "Select",
                    Answer2 = "Where",
                    Answer3 = "OrderBy",
                    Answer4 = "GroupBy",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What is an interface in C#?",
                    Answer1 = "A class with implemented methods",
                    Answer2 = "A contract that defines members without implementation",
                    Answer3 = "A type of enum",
                    Answer4 = "A sealed struct",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "Which access modifier allows access only within the same class?",
                    Answer1 = "public",
                    Answer2 = "internal",
                    Answer3 = "protected",
                    Answer4 = "private",
                    CorrectAnswerNumber = 4,
                    Difficulty = 1,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What is the purpose of the 'async' keyword in C#?",
                    Answer1 = "It creates a new thread",
                    Answer2 = "It marks a method as capable of using await",
                    Answer3 = "It makes code execute synchronously",
                    Answer4 = "It locks the method",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What does the 'using' statement do when used with IDisposable objects?",
                    Answer1 = "Imports namespaces only",
                    Answer2 = "Creates a loop",
                    Answer3 = "Ensures the object is disposed",
                    Answer4 = "Makes the object global",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },
                new() {
                    Text = "What happens when an exception is thrown and not caught in a method?",
                    Answer1 = "The method silently ignores it",
                    Answer2 = "The exception propagates up the call stack",
                    Answer3 = "The CLR always restarts the application",
                    Answer4 = "The method returns null",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[0].Id,
                    Technology = techArray[0]
                },

                // JavaScript / TypeScript — techArray[1]

                new() {
                    Text = "Why is '=== ' generally preferred over '==' in JavaScript?",
                    Answer1 = "Because it performs type coercion more efficiently",
                    Answer2 = "Because it avoids implicit type conversion during comparison",
                    Answer3 = "Because it only works for strings",
                    Answer4 = "Because it compares object references by value",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is a common reason to prefer 'unknown' over 'any' in TypeScript?",
                    Answer1 = "Unknown disables all type checks",
                    Answer2 = "Unknown forces safer narrowing before use",
                    Answer3 = "Unknown can only hold numbers",
                    Answer4 = "Unknown is faster at runtime",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What problem do closures help solve in JavaScript?",
                    Answer1 = "They allow functions to retain access to variables from their lexical scope",
                    Answer2 = "They automatically freeze objects",
                    Answer3 = "They remove the need for promises",
                    Answer4 = "They convert arrays to objects",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Why can mutating objects directly in frontend state management be risky?",
                    Answer1 = "Because JavaScript forbids mutation",
                    Answer2 = "Because change detection and debugging become harder",
                    Answer3 = "Because mutated objects become null",
                    Answer4 = "Because objects can no longer be serialized",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What does 'Promise.race()' do?",
                    Answer1 = "Waits for all promises to resolve",
                    Answer2 = "Returns the result of the first promise to settle",
                    Answer3 = "Runs promises sequentially",
                    Answer4 = "Cancels all slower promises automatically",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is the main benefit of optional chaining '?.'?",
                    Answer1 = "It converts undefined into false automatically",
                    Answer2 = "It safely accesses nested properties when intermediate values may be null or undefined",
                    Answer3 = "It deep-clones an object",
                    Answer4 = "It forces strict typing at runtime",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which statement about interfaces and type aliases in TypeScript is most accurate?",
                    Answer1 = "They are always identical in every scenario",
                    Answer2 = "Both can describe shapes, but they have different capabilities and ergonomics in some cases",
                    Answer3 = "Interfaces can describe primitives only",
                    Answer4 = "Type aliases cannot represent unions",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Why can using 'var' be problematic in JavaScript?",
                    Answer1 = "Because it is block-scoped",
                    Answer2 = "Because it is function-scoped and can lead to unexpected behavior compared to let/const",
                    Answer3 = "Because it cannot store objects",
                    Answer4 = "Because modern browsers do not support it",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is the purpose of a discriminated union in TypeScript?",
                    Answer1 = "To make all types mutable",
                    Answer2 = "To model variants safely using a shared discriminant field",
                    Answer3 = "To disable narrowing",
                    Answer4 = "To replace interfaces with enums only",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Why might a developer use 'Map' instead of a plain object in JavaScript?",
                    Answer1 = "Because Map supports key types beyond strings and has dedicated APIs",
                    Answer2 = "Because Map is always faster in every case",
                    Answer3 = "Because objects cannot store values",
                    Answer4 = "Because Map is immutable by default",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is event bubbling in the browser?",
                    Answer1 = "An event traveling from the target element up through its ancestors",
                    Answer2 = "An event being cancelled automatically",
                    Answer3 = "An event executing only on window",
                    Answer4 = "An event that repeats every second",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is the main advantage of generics in TypeScript?",
                    Answer1 = "They add runtime reflection automatically",
                    Answer2 = "They allow reusable code while preserving type safety",
                    Answer3 = "They make transpilation unnecessary",
                    Answer4 = "They replace all interfaces",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is the primary benefit of TypeScript over JavaScript?",
                    Answer1 = "Faster runtime",
                    Answer2 = "Static type checking",
                    Answer3 = "Smaller bundle size",
                    Answer4 = "Better DOM access",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which keyword is used to define an interface in TypeScript?",
                    Answer1 = "type",
                    Answer2 = "interface",
                    Answer3 = "class",
                    Answer4 = "struct",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is a generic type in TypeScript?",
                    Answer1 = "A type that works with different data types",
                    Answer2 = "A type that is always string",
                    Answer3 = "A type alias only",
                    Answer4 = "A runtime type check",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What does the 'readonly' modifier do in TypeScript?",
                    Answer1 = "Makes a property required",
                    Answer2 = "Prevents reassignment after initialization",
                    Answer3 = "Makes a property optional",
                    Answer4 = "Removes the property",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which utility type makes all properties optional?",
                    Answer1 = "Required<T>",
                    Answer2 = "Partial<T>",
                    Answer3 = "Pick<T>",
                    Answer4 = "Readonly<T>",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is a union type in TypeScript?",
                    Answer1 = "A type for arrays only",
                    Answer2 = "A type that can be one of several types",
                    Answer3 = "A combination of two objects at runtime",
                    Answer4 = "A special numeric type",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What does the 'never' type represent?",
                    Answer1 = "A null value",
                    Answer2 = "An undefined value",
                    Answer3 = "A value that never occurs",
                    Answer4 = "Any value",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "How do you type a function that returns nothing in TypeScript?",
                    Answer1 = "null",
                    Answer2 = "undefined",
                    Answer3 = "void",
                    Answer4 = "never",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is type narrowing?",
                    Answer1 = "Reducing a union to a more specific type",
                    Answer2 = "Making a file smaller",
                    Answer3 = "Removing type annotations",
                    Answer4 = "Converting all values to strings",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is a tuple in TypeScript?",
                    Answer1 = "A dynamic array of any length",
                    Answer2 = "A fixed-length array with known element types",
                    Answer3 = "A key-value object",
                    Answer4 = "A set of unique values",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which keyword declares a block-scoped variable in JavaScript?",
                    Answer1 = "var",
                    Answer2 = "let",
                    Answer3 = "define",
                    Answer4 = "static",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is the result of 'typeof null' in JavaScript?",
                    Answer1 = "null",
                    Answer2 = "undefined",
                    Answer3 = "object",
                    Answer4 = "boolean",
                    CorrectAnswerNumber = 3,
                    Difficulty = 3,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which JavaScript method adds an element to the end of an array?",
                    Answer1 = "shift()",
                    Answer2 = "push()",
                    Answer3 = "pop()",
                    Answer4 = "unshift()",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is the result of '===' in JavaScript?",
                    Answer1 = "Assignment",
                    Answer2 = "Loose equality comparison",
                    Answer3 = "Strict equality comparison",
                    Answer4 = "Type conversion",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which TypeScript feature allows a property to be omitted?",
                    Answer1 = "readonly",
                    Answer2 = "optional property with '?'",
                    Answer3 = "never",
                    Answer4 = "const",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What does 'const' guarantee for an object in JavaScript?",
                    Answer1 = "The object's contents cannot change",
                    Answer2 = "The variable cannot be reassigned",
                    Answer3 = "The object becomes immutable",
                    Answer4 = "The object is deeply frozen",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which array method creates a new array by transforming each element?",
                    Answer1 = "filter()",
                    Answer2 = "reduce()",
                    Answer3 = "map()",
                    Answer4 = "find()",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What is the purpose of 'Promise.all()'?",
                    Answer1 = "It runs only the first promise",
                    Answer2 = "It waits for all promises to resolve or one to reject",
                    Answer3 = "It converts promises to callbacks",
                    Answer4 = "It delays promise execution",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which TypeScript utility type picks a subset of properties from a type?",
                    Answer1 = "Partial<T>",
                    Answer2 = "Record<K, T>",
                    Answer3 = "Pick<T, K>",
                    Answer4 = "Exclude<T, U>",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What does 'async' before a function mean in JavaScript?",
                    Answer1 = "The function always runs on a new thread",
                    Answer2 = "The function returns a Promise",
                    Answer3 = "The function is executed synchronously",
                    Answer4 = "The function blocks the event loop",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which statement about 'any' in TypeScript is correct?",
                    Answer1 = "It disables type checking for that value",
                    Answer2 = "It is safer than unknown in all cases",
                    Answer3 = "It can only store strings",
                    Answer4 = "It is the same as never",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What does the spread operator '...' do in JavaScript?",
                    Answer1 = "It comments out code",
                    Answer2 = "It expands iterable or object values",
                    Answer3 = "It compares objects",
                    Answer4 = "It creates a Promise",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "Which TypeScript type is safer than 'any' because it requires narrowing before use?",
                    Answer1 = "void",
                    Answer2 = "never",
                    Answer3 = "unknown",
                    Answer4 = "object",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },
                new() {
                    Text = "What does 'filter()' return in JavaScript?",
                    Answer1 = "The first matching element",
                    Answer2 = "A new array with elements that pass the test",
                    Answer3 = "A boolean",
                    Answer4 = "The number of matching elements",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[1].Id,
                    Technology = techArray[1]
                },

                // C/C++ — techArray[2]

                new() {
                    Text = "Why is manual memory management in C/C++ considered error-prone?",
                    Answer1 = "Because the compiler always ignores allocation failures",
                    Answer2 = "Because developers must ensure correct ownership and deallocation themselves",
                    Answer3 = "Because stack memory cannot be used",
                    Answer4 = "Because pointers are forbidden in production code",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is the main benefit of RAII in C++?",
                    Answer1 = "It delays all cleanup until program exit",
                    Answer2 = "It ties resource lifetime to object lifetime for safer cleanup",
                    Answer3 = "It eliminates the need for constructors",
                    Answer4 = "It prevents the use of exceptions",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Why are smart pointers preferred over raw owning pointers in modern C++?",
                    Answer1 = "Because smart pointers are stored only on the stack",
                    Answer2 = "Because they express ownership and reduce memory management mistakes",
                    Answer3 = "Because raw pointers cannot point to objects",
                    Answer4 = "Because smart pointers are always faster than raw pointers",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is undefined behavior in C/C++ a warning sign for?",
                    Answer1 = "Portable, guaranteed program logic",
                    Answer2 = "Code whose result is not guaranteed by the language standard",
                    Answer3 = "Automatic exception handling",
                    Answer4 = "Compiler optimization being disabled",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Why can buffer overflows be dangerous in C/C++ programs?",
                    Answer1 = "Because they only slow down sorting algorithms",
                    Answer2 = "Because they can corrupt memory and cause security vulnerabilities",
                    Answer3 = "Because they only affect comments",
                    Answer4 = "Because they prevent compilation",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is the difference between a reference and a pointer in C++?",
                    Answer1 = "References are always reseatable like pointers",
                    Answer2 = "Pointers can be null and reseated; references are typically bound to an object and not reseated",
                    Answer3 = "References require manual delete",
                    Answer4 = "Pointers cannot be passed to functions",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Why is const-correctness important in C++ APIs?",
                    Answer1 = "It documents intent and helps prevent accidental mutation",
                    Answer2 = "It makes every object immutable forever",
                    Answer3 = "It replaces unit tests",
                    Answer4 = "It guarantees multithreaded safety automatically",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What problem does std::vector usually solve better than a raw dynamic array?",
                    Answer1 = "It removes the need for contiguous storage",
                    Answer2 = "It manages size and memory more safely and conveniently",
                    Answer3 = "It guarantees no reallocations ever",
                    Answer4 = "It stores mixed types without wrappers",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is a dangling pointer?",
                    Answer1 = "A pointer to valid live memory",
                    Answer2 = "A pointer that refers to memory that is no longer valid",
                    Answer3 = "A pointer to a constant",
                    Answer4 = "A pointer with address zero",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Why can mixing 'new/delete' with 'malloc/free' be problematic in C++?",
                    Answer1 = "Because they follow different allocation and construction/destruction models",
                    Answer2 = "Because the compiler automatically converts one into the other",
                    Answer3 = "Because malloc calls constructors",
                    Answer4 = "Because delete can free only stack memory",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is the main reason to pass large objects by const reference in C++?",
                    Answer1 = "To force heap allocation",
                    Answer2 = "To avoid unnecessary copying while preventing modification",
                    Answer3 = "To make the function asynchronous",
                    Answer4 = "To convert the object into a pointer automatically",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is one major advantage of templates in C++?",
                    Answer1 = "They allow generic programming without sacrificing type safety",
                    Answer2 = "They run only at runtime",
                    Answer3 = "They replace the STL completely",
                    Answer4 = "They avoid all code generation",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which header is commonly used for input and output streams in C++?",
                    Answer1 = "<stdio.h>",
                    Answer2 = "<iostream>",
                    Answer3 = "<stream>",
                    Answer4 = "<inputoutput>",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What does 'nullptr' represent in modern C++?",
                    Answer1 = "An empty string",
                    Answer2 = "A null pointer literal",
                    Answer3 = "A zero-length array",
                    Answer4 = "An invalid reference",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which keyword is used to define a constant variable in C/C++?",
                    Answer1 = "readonly",
                    Answer2 = "fixed",
                    Answer3 = "const",
                    Answer4 = "immutable",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is the purpose of 'delete' in C++?",
                    Answer1 = "To remove a function from a class",
                    Answer2 = "To free memory allocated with new",
                    Answer3 = "To clear stack variables",
                    Answer4 = "To destroy all pointers automatically",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What does a reference in C++ provide?",
                    Answer1 = "A second name for an existing object",
                    Answer2 = "A dynamically allocated object",
                    Answer3 = "A null-safe pointer",
                    Answer4 = "A copy of a variable",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which container in the C++ Standard Library stores elements in dynamic contiguous memory?",
                    Answer1 = "std::map",
                    Answer2 = "std::queue",
                    Answer3 = "std::vector",
                    Answer4 = "std::stack",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is the purpose of 'const' after a member function declaration in C++?",
                    Answer1 = "It makes the function static",
                    Answer2 = "It promises not to modify the object's observable state",
                    Answer3 = "It makes the function private",
                    Answer4 = "It returns a constant value",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which statement about arrays in C is correct?",
                    Answer1 = "They know their length automatically in every function",
                    Answer2 = "They are always dynamically sized",
                    Answer3 = "They store elements of the same type contiguously",
                    Answer4 = "They can contain mixed types",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What can happen if you access memory outside array bounds in C/C++?",
                    Answer1 = "The compiler always fixes it",
                    Answer2 = "It results in undefined behavior",
                    Answer3 = "It always throws an exception",
                    Answer4 = "The program safely ignores it",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which operator dereferences a pointer?",
                    Answer1 = "&",
                    Answer2 = "*",
                    Answer3 = "->",
                    Answer4 = "::",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is function overloading in C++?",
                    Answer1 = "Having functions with the same name but different parameters",
                    Answer2 = "Calling a function recursively",
                    Answer3 = "Overwriting a function file",
                    Answer4 = "Running too many functions at once",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Why are smart pointers used in modern C++?",
                    Answer1 = "To replace all classes",
                    Answer2 = "To manage resource ownership more safely",
                    Answer3 = "To make pointers faster than references",
                    Answer4 = "To allocate only stack memory",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What symbol is used to declare a pointer in C/C++?",
                    Answer1 = "&",
                    Answer2 = "*",
                    Answer3 = "#",
                    Answer4 = "%",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which operator is used to get the address of a variable?",
                    Answer1 = "*",
                    Answer2 = "&",
                    Answer3 = "->",
                    Answer4 = "::",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which keyword allocates memory dynamically in C++?",
                    Answer1 = "alloc",
                    Answer2 = "malloc",
                    Answer3 = "new",
                    Answer4 = "create",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which function is traditionally used for dynamic memory allocation in C?",
                    Answer1 = "alloc",
                    Answer2 = "malloc",
                    Answer3 = "new",
                    Answer4 = "create",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What does RAII stand for in C++?",
                    Answer1 = "Resource Allocation Is Initialization",
                    Answer2 = "Runtime Allocation In Interface",
                    Answer3 = "Resource Access In Inheritance",
                    Answer4 = "Runtime Access Is Initialization",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which of these is used to define a class in C++?",
                    Answer1 = "class",
                    Answer2 = "type",
                    Answer3 = "object",
                    Answer4 = "structonly",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is the main purpose of a destructor in C++?",
                    Answer1 = "To construct objects",
                    Answer2 = "To free resources when an object is destroyed",
                    Answer3 = "To overload operators",
                    Answer4 = "To define inheritance",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which symbol is commonly used to access a member through a pointer in C++?",
                    Answer1 = ".",
                    Answer2 = "::",
                    Answer3 = "->",
                    Answer4 = "#",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is a memory leak?",
                    Answer1 = "When memory is automatically optimized",
                    Answer2 = "When allocated memory is not released and becomes unusable",
                    Answer3 = "When stack variables are reused",
                    Answer4 = "When a pointer points to a valid object",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is the difference between stack and heap memory in C/C++?",
                    Answer1 = "Both are always manually managed",
                    Answer2 = "Stack is used for local variables; heap is used for dynamic allocation",
                    Answer3 = "Heap is faster and fixed-size only",
                    Answer4 = "Stack stores only pointers",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "What is undefined behavior in C/C++?",
                    Answer1 = "Behavior fully defined by the standard",
                    Answer2 = "Behavior that always throws an exception",
                    Answer3 = "Behavior for which the language standard imposes no requirements",
                    Answer4 = "Behavior that only occurs in debug mode",
                    CorrectAnswerNumber = 3,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },
                new() {
                    Text = "Which of the following best describes a dangling pointer?",
                    Answer1 = "A pointer initialized to zero",
                    Answer2 = "A pointer that refers to memory that is no longer valid",
                    Answer3 = "A pointer to a constant value",
                    Answer4 = "A pointer stored on the stack",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[2].Id,
                    Technology = techArray[2]
                },

                // SQL — techArray[3]

                new() {
                    Text = "Why can SELECT * be a poor choice in production queries?",
                    Answer1 = "Because SQL does not allow it in joins",
                    Answer2 = "Because it may fetch unnecessary columns and make code more fragile",
                    Answer3 = "Because it always causes deadlocks",
                    Answer4 = "Because it prevents WHERE clauses",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is the main purpose of an index?",
                    Answer1 = "To duplicate a table for backup",
                    Answer2 = "To speed up certain query patterns at the cost of extra storage and write overhead",
                    Answer3 = "To guarantee that all queries are fast",
                    Answer4 = "To replace normalization",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Why can too many indexes hurt performance?",
                    Answer1 = "Because indexes are ignored by the optimizer",
                    Answer2 = "Because writes and maintenance can become more expensive",
                    Answer3 = "Because indexed tables cannot be joined",
                    Answer4 = "Because indexes prevent sorting",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What problem does normalization primarily try to reduce?",
                    Answer1 = "CPU frequency fluctuations",
                    Answer2 = "Data redundancy and update anomalies",
                    Answer3 = "Network latency only",
                    Answer4 = "SQL syntax errors",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "When can denormalization be considered?",
                    Answer1 = "Never, because it is always wrong",
                    Answer2 = "When read performance or reporting needs justify controlled redundancy",
                    Answer3 = "Only for temporary tables",
                    Answer4 = "Only when no primary keys exist",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is the difference between WHERE and HAVING?",
                    Answer1 = "They are always interchangeable",
                    Answer2 = "WHERE filters rows before grouping; HAVING filters groups after aggregation",
                    Answer3 = "HAVING works only with strings",
                    Answer4 = "WHERE can be used only with ORDER BY",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Why are transactions important in databases?",
                    Answer1 = "They make all queries faster",
                    Answer2 = "They help preserve data consistency across multiple operations",
                    Answer3 = "They eliminate the need for indexes",
                    Answer4 = "They automatically normalize tables",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is a deadlock in SQL systems?",
                    Answer1 = "A syntax error in a join",
                    Answer2 = "A situation where two or more transactions wait on each other indefinitely",
                    Answer3 = "A failed index rebuild",
                    Answer4 = "A table without a primary key",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Why is parameterized SQL preferred over string-concatenated SQL?",
                    Answer1 = "Because parameterization helps prevent SQL injection and improves safety",
                    Answer2 = "Because concatenation is not supported in SQL",
                    Answer3 = "Because parameters automatically create indexes",
                    Answer4 = "Because parameterized queries cannot fail",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is the main advantage of a primary key?",
                    Answer1 = "It automatically encrypts a table",
                    Answer2 = "It uniquely identifies rows and supports relational integrity",
                    Answer3 = "It eliminates the need for joins",
                    Answer4 = "It guarantees perfect query performance",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which SQL statement is used to retrieve data from a table?",
                    Answer1 = "GET",
                    Answer2 = "SELECT",
                    Answer3 = "READ",
                    Answer4 = "FETCHROW",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which clause is used to filter rows before grouping?",
                    Answer1 = "HAVING",
                    Answer2 = "ORDER BY",
                    Answer3 = "WHERE",
                    Answer4 = "GROUP BY",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which clause is used to filter grouped results?",
                    Answer1 = "WHERE",
                    Answer2 = "HAVING",
                    Answer3 = "ORDER BY",
                    Answer4 = "LIMIT",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which JOIN returns all matching rows from both tables when possible, and unmatched rows from the left table as well?",
                    Answer1 = "INNER JOIN",
                    Answer2 = "RIGHT JOIN",
                    Answer3 = "LEFT JOIN",
                    Answer4 = "CROSS JOIN",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What does the COUNT(*) function do?",
                    Answer1 = "Counts only non-null values in one column",
                    Answer2 = "Counts all rows",
                    Answer3 = "Counts only distinct rows",
                    Answer4 = "Counts only indexed rows",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is the purpose of an index in SQL?",
                    Answer1 = "To encrypt data",
                    Answer2 = "To improve query performance for certain lookups",
                    Answer3 = "To guarantee no duplicates in every column",
                    Answer4 = "To replace primary keys",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which SQL command is used to modify existing rows?",
                    Answer1 = "MODIFY",
                    Answer2 = "CHANGE",
                    Answer3 = "UPDATE",
                    Answer4 = "ALTER",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What does a PRIMARY KEY do?",
                    Answer1 = "Allows null values",
                    Answer2 = "Uniquely identifies each row in a table",
                    Answer3 = "Stores encrypted data only",
                    Answer4 = "Sorts the table automatically",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is normalization in database design?",
                    Answer1 = "Adding more duplicate data for speed",
                    Answer2 = "Organizing data to reduce redundancy and improve integrity",
                    Answer3 = "Converting numbers to strings",
                    Answer4 = "Encrypting related tables",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is a transaction in SQL?",
                    Answer1 = "A permanent table structure",
                    Answer2 = "A sequence of operations treated as a single unit of work",
                    Answer3 = "A type of join",
                    Answer4 = "A read-only query",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which SQL clause is used to sort query results?",
                    Answer1 = "GROUP BY",
                    Answer2 = "ORDER BY",
                    Answer3 = "HAVING",
                    Answer4 = "SORT BY",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which aggregate function returns the average value?",
                    Answer1 = "COUNT()",
                    Answer2 = "SUM()",
                    Answer3 = "AVG()",
                    Answer4 = "MEAN()",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What does INNER JOIN return?",
                    Answer1 = "All rows from the left table only",
                    Answer2 = "Only rows that match in both tables",
                    Answer3 = "All possible combinations of rows",
                    Answer4 = "Only rows with null values",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which SQL command adds new rows to a table?",
                    Answer1 = "ADD",
                    Answer2 = "APPEND",
                    Answer3 = "INSERT",
                    Answer4 = "PUT",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What does DISTINCT do in SQL?",
                    Answer1 = "Sorts rows descending",
                    Answer2 = "Removes duplicate rows from the result",
                    Answer3 = "Deletes repeated rows from the table",
                    Answer4 = "Creates an index",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which constraint prevents duplicate values and usually disallows nulls?",
                    Answer1 = "CHECK",
                    Answer2 = "DEFAULT",
                    Answer3 = "UNIQUE",
                    Answer4 = "INDEX",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is a foreign key used for?",
                    Answer1 = "To encrypt a column",
                    Answer2 = "To create a relationship between tables",
                    Answer3 = "To sort rows automatically",
                    Answer4 = "To store JSON data",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which SQL statement removes a table definition and its data?",
                    Answer1 = "DELETE TABLE",
                    Answer2 = "REMOVE TABLE",
                    Answer3 = "DROP TABLE",
                    Answer4 = "TRUNCATE TABLE",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "What is the difference between DELETE and TRUNCATE in SQL?",
                    Answer1 = "There is no difference",
                    Answer2 = "DELETE removes selected rows; TRUNCATE removes all rows more directly",
                    Answer3 = "TRUNCATE can use WHERE; DELETE cannot",
                    Answer4 = "DELETE removes the table structure",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },
                new() {
                    Text = "Which normal form aims to eliminate partial dependency on a composite key?",
                    Answer1 = "First Normal Form",
                    Answer2 = "Second Normal Form",
                    Answer3 = "Third Normal Form",
                    Answer4 = "Boyce-Codd Normal Form",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[3].Id,
                    Technology = techArray[3]
                },

                // .NET — techArray[4]

                new() {
                    Text = "Why is dependency injection valuable in ASP.NET Core applications?",
                    Answer1 = "It reduces flexibility by forcing concrete dependencies everywhere",
                    Answer2 = "It improves modularity, testability, and separation of concerns",
                    Answer3 = "It removes the need for configuration",
                    Answer4 = "It replaces middleware",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is the main risk of registering a non-thread-safe service as Singleton when it should be Scoped?",
                    Answer1 = "The application will refuse to start in all cases",
                    Answer2 = "Shared state may cause bugs across concurrent requests",
                    Answer3 = "The service becomes read-only",
                    Answer4 = "It disables model binding",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Why is middleware order important in ASP.NET Core?",
                    Answer1 = "Because middleware components form a pipeline where earlier components affect later behavior",
                    Answer2 = "Because the runtime alphabetically reorders middleware",
                    Answer3 = "Because order matters only for static files",
                    Answer4 = "Because only the last middleware executes",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is a good reason to separate DTOs from domain or entity models in Web APIs?",
                    Answer1 = "To make serialization impossible",
                    Answer2 = "To control API contracts and avoid leaking internal persistence details",
                    Answer3 = "To remove validation logic permanently",
                    Answer4 = "To force all properties to be public fields",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Why is async I/O beneficial in high-concurrency web applications?",
                    Answer1 = "Because it makes every database query faster internally",
                    Answer2 = "Because it helps avoid blocking request threads while waiting for I/O",
                    Answer3 = "Because it guarantees zero allocations",
                    Answer4 = "Because it disables the thread pool",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is one main purpose of middleware such as exception handling middleware?",
                    Answer1 = "To generate SQL indexes",
                    Answer2 = "To centralize cross-cutting concerns in the request pipeline",
                    Answer3 = "To replace controllers",
                    Answer4 = "To remove HTTP status codes",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Why can exposing EF Core entities directly from API endpoints be risky?",
                    Answer1 = "Because EF Core entities cannot be serialized",
                    Answer2 = "Because it couples API contracts to persistence models and may expose unwanted data",
                    Answer3 = "Because entities always contain circular references",
                    Answer4 = "Because APIs only support tuples",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is the benefit of using configuration providers in .NET?",
                    Answer1 = "They allow flexible configuration from multiple sources",
                    Answer2 = "They remove the need for environments",
                    Answer3 = "They compile settings into machine code",
                    Answer4 = "They make secrets safe to commit to source control",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Why should secrets generally not be stored in appsettings.json for production?",
                    Answer1 = "Because JSON cannot represent strings",
                    Answer2 = "Because configuration files may be exposed or committed, so dedicated secret storage is safer",
                    Answer3 = "Because ASP.NET Core refuses to read appsettings in production",
                    Answer4 = "Because secrets can only exist in code constants",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is the main reason to use health checks in production services?",
                    Answer1 = "To increase CPU usage deliberately",
                    Answer2 = "To provide visibility into application readiness and liveness",
                    Answer3 = "To replace logging",
                    Answer4 = "To avoid using HTTP",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Which file contains the entry point configuration for many modern minimal ASP.NET Core apps?",
                    Answer1 = "Program.cs",
                    Answer2 = "Startup.xml",
                    Answer3 = "App.xaml",
                    Answer4 = "Global.asax",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is Kestrel in ASP.NET Core?",
                    Answer1 = "A database provider",
                    Answer2 = "A cross-platform web server",
                    Answer3 = "A logging library",
                    Answer4 = "An ORM tool",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What does configuration in .NET commonly support?",
                    Answer1 = "Only XML files",
                    Answer2 = "JSON, environment variables, command-line args, and more",
                    Answer3 = "Only hardcoded constants",
                    Answer4 = "Only appsettings.production.txt",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Why is dependency injection useful in .NET applications?",
                    Answer1 = "It makes all code static",
                    Answer2 = "It improves coupling between classes",
                    Answer3 = "It helps manage dependencies and improves testability",
                    Answer4 = "It removes the need for interfaces",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is model binding in ASP.NET Core?",
                    Answer1 = "Binding SQL tables together",
                    Answer2 = "Mapping request data to action parameters or models",
                    Answer3 = "Generating HTML views automatically",
                    Answer4 = "Registering services in DI",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Which result is commonly returned from a Web API action for a successful resource creation?",
                    Answer1 = "404 Not Found",
                    Answer2 = "201 Created",
                    Answer3 = "500 Internal Server Error",
                    Answer4 = "401 Unauthorized",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is the purpose of app.UseAuthorization()?",
                    Answer1 = "It validates user permissions and policies",
                    Answer2 = "It hashes passwords",
                    Answer3 = "It opens database connections",
                    Answer4 = "It enables CORS automatically",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is a hosted service in .NET commonly used for?",
                    Answer1 = "Rendering Razor views",
                    Answer2 = "Running background tasks",
                    Answer3 = "Creating database migrations only",
                    Answer4 = "Managing CSS files",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Which attribute is commonly used to map an HTTP GET request to an API action?",
                    Answer1 = "[Get]",
                    Answer2 = "[HttpGet]",
                    Answer3 = "[RouteGet]",
                    Answer4 = "[ApiGet]",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Why should a DbContext in EF Core usually not be registered as a singleton?",
                    Answer1 = "Because it is designed to be shared by all requests forever",
                    Answer2 = "Because it is typically scoped and not thread-safe for broad shared use",
                    Answer3 = "Because singleton is faster",
                    Answer4 = "Because EF Core only works in console apps",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Which framework is commonly used to build web APIs in modern .NET?",
                    Answer1 = "ASP.NET Core",
                    Answer2 = "WinForms",
                    Answer3 = "WPF",
                    Answer4 = "MAUI only",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is middleware in ASP.NET Core?",
                    Answer1 = "A UI library",
                    Answer2 = "A component in the HTTP request pipeline",
                    Answer3 = "A database engine",
                    Answer4 = "A testing framework",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Which built-in container feature is heavily used in ASP.NET Core?",
                    Answer1 = "Reflection emit",
                    Answer2 = "Dependency injection",
                    Answer3 = "Binary serialization",
                    Answer4 = "Thread abortion",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is Entity Framework Core primarily used for?",
                    Answer1 = "Frontend rendering",
                    Answer2 = "Object-relational mapping",
                    Answer3 = "Image processing",
                    Answer4 = "Network monitoring",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Which file commonly contains ASP.NET Core configuration settings?",
                    Answer1 = "settings.xml",
                    Answer2 = "appsettings.json",
                    Answer3 = "config.ini",
                    Answer4 = "runtime.yaml",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What does the built-in logging abstraction in .NET help you do?",
                    Answer1 = "Compile faster",
                    Answer2 = "Write logs through interchangeable providers",
                    Answer3 = "Avoid exceptions completely",
                    Answer4 = "Eliminate dependency injection",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is the purpose of app.UseAuthentication() in ASP.NET Core?",
                    Answer1 = "It enables authentication middleware",
                    Answer2 = "It creates user accounts",
                    Answer3 = "It encrypts all database values",
                    Answer4 = "It starts a background job",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "What is a migration in Entity Framework Core?",
                    Answer1 = "A way to move UI components",
                    Answer2 = "A mechanism for evolving the database schema",
                    Answer3 = "A way to restart the application",
                    Answer4 = "A logging strategy",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Which service lifetime creates one instance per HTTP request in ASP.NET Core?",
                    Answer1 = "Singleton",
                    Answer2 = "Transient",
                    Answer3 = "Scoped",
                    Answer4 = "Static",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },
                new() {
                    Text = "Why is middleware order important in ASP.NET Core?",
                    Answer1 = "Because middleware runs in the order it is added and can affect later components",
                    Answer2 = "Because the CLR sorts it alphabetically",
                    Answer3 = "Because only the first middleware ever runs",
                    Answer4 = "Because order matters only in development",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[4].Id,
                    Technology = techArray[4]
                },

                // React — techArray[5]

                new() {
                    Text = "Why is immutability especially important in React state management?",
                    Answer1 = "Because React can more reliably detect changes when new references are created",
                    Answer2 = "Because JavaScript objects are immutable by default",
                    Answer3 = "Because mutable state cannot be rendered",
                    Answer4 = "Because hooks require primitive values only",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is a common reason for unnecessary re-renders in React?",
                    Answer1 = "Stable props and stable callbacks",
                    Answer2 = "Creating new object or function references on every render",
                    Answer3 = "Using JSX in components",
                    Answer4 = "Returning null from a component",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "When can useMemo be helpful?",
                    Answer1 = "When memoizing expensive calculations whose dependencies do not change often",
                    Answer2 = "As a replacement for all state logic",
                    Answer3 = "To trigger side effects after every render",
                    Answer4 = "To guarantee that child components never render",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Why should useEffect cleanup functions be used when appropriate?",
                    Answer1 = "To rename component props automatically",
                    Answer2 = "To avoid leaks or stale subscriptions such as timers and event listeners",
                    Answer3 = "To prevent JSX compilation",
                    Answer4 = "To make components class-based",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What problem does lifting state up solve?",
                    Answer1 = "It reduces bundle size by compressing state",
                    Answer2 = "It lets multiple related components share a common source of truth",
                    Answer3 = "It turns local state into server state",
                    Answer4 = "It eliminates props completely",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Why can using array index as a React key cause bugs?",
                    Answer1 = "Because keys cannot be numbers",
                    Answer2 = "Because identity can become unstable when items are inserted, removed, or reordered",
                    Answer3 = "Because it disables hooks",
                    Answer4 = "Because React ignores numeric keys",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is the main purpose of React.memo?",
                    Answer1 = "To memoize a component result based on props and avoid some unnecessary renders",
                    Answer2 = "To replace useEffect",
                    Answer3 = "To make state mutable",
                    Answer4 = "To fetch cached server responses automatically",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "When might useReducer be preferred over useState?",
                    Answer1 = "When state transitions are more complex and benefit from explicit action-based updates",
                    Answer2 = "Only when working with CSS",
                    Answer3 = "Only in class components",
                    Answer4 = "When the component has no state",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is a controlled input in React?",
                    Answer1 = "An input whose value is driven by React state",
                    Answer2 = "An input managed only by the browser with no props",
                    Answer3 = "An input that cannot be edited",
                    Answer4 = "An input created with useMemo",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Why can stale closures cause bugs in React components?",
                    Answer1 = "Because functions may capture outdated values from earlier renders",
                    Answer2 = "Because closures exist only in TypeScript",
                    Answer3 = "Because React forbids nested functions",
                    Answer4 = "Because closures always trigger infinite loops",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What does separation of presentational and stateful concerns usually improve in React codebases?",
                    Answer1 = "Only CSS loading speed",
                    Answer2 = "Readability, reusability, and maintainability",
                    Answer3 = "Browser compatibility with IE6 only",
                    Answer4 = "It removes the need for hooks",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Why can excessive context usage become a performance concern?",
                    Answer1 = "Because every context change can re-render many consumers",
                    Answer2 = "Because context works only on the server",
                    Answer3 = "Because context cannot store objects",
                    Answer4 = "Because context disables routing",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What does React use to decide which parts of the UI need updating efficiently?",
                    Answer1 = "Direct DOM scanning only",
                    Answer2 = "Virtual DOM reconciliation",
                    Answer3 = "Manual HTML diff files",
                    Answer4 = "CSS compilation",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Which prop is commonly used to pass nested UI content into a component?",
                    Answer1 = "content",
                    Answer2 = "children",
                    Answer3 = "body",
                    Answer4 = "template",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is the purpose of useRef in React?",
                    Answer1 = "To automatically re-render on every change",
                    Answer2 = "To hold a mutable value or DOM reference without causing a re-render",
                    Answer3 = "To replace all state usage",
                    Answer4 = "To fetch data from the server",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Which statement about state updates in React is correct?",
                    Answer1 = "State should be mutated directly",
                    Answer2 = "State updates should create new values instead of mutating existing ones",
                    Answer3 = "State can only contain strings",
                    Answer4 = "State is global by default",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is a custom hook in React?",
                    Answer1 = "A class component with extra methods",
                    Answer2 = "A regular function that uses React hooks to share logic",
                    Answer3 = "A Redux reducer",
                    Answer4 = "A browser API",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "When does useEffect run if its dependency array is empty?",
                    Answer1 = "On every render",
                    Answer2 = "Only after the initial render",
                    Answer3 = "Only before rendering",
                    Answer4 = "Never",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Why is immutability important in React state updates?",
                    Answer1 = "Because React forbids objects",
                    Answer2 = "Because it helps React detect changes more reliably",
                    Answer3 = "Because JavaScript arrays are immutable",
                    Answer4 = "Because it prevents all bugs",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is prop drilling?",
                    Answer1 = "Rendering props in a loop",
                    Answer2 = "Passing props through many intermediate components",
                    Answer3 = "Storing props in local storage",
                    Answer4 = "Debugging props with DevTools",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is the main purpose of React.memo?",
                    Answer1 = "To fetch memoized data from an API",
                    Answer2 = "To avoid unnecessary re-renders when props have not changed",
                    Answer3 = "To replace useState",
                    Answer4 = "To style components automatically",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Which hook is useful when state logic becomes more complex than simple setters?",
                    Answer1 = "useEffect",
                    Answer2 = "useRef",
                    Answer3 = "useReducer",
                    Answer4 = "useLayoutEffect",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What should usually be used for navigation in a React SPA?",
                    Answer1 = "Full page reloads with window.location for every click",
                    Answer2 = "A client-side router such as react-router",
                    Answer3 = "Only HTML frames",
                    Answer4 = "Database redirects",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Why can using array index as a key sometimes be problematic?",
                    Answer1 = "Because React does not allow numeric keys",
                    Answer2 = "Because it can lead to unstable identity when list items are reordered",
                    Answer3 = "Because indexes are too slow to calculate",
                    Answer4 = "Because keys are only needed in TypeScript",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Which hook is used to manage local state in a functional React component?",
                    Answer1 = "useState",
                    Answer2 = "useEffect",
                    Answer3 = "useMemo",
                    Answer4 = "useContext",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What does JSX stand for?",
                    Answer1 = "JavaScript XML",
                    Answer2 = "Java Syntax Extension",
                    Answer3 = "JSON XML",
                    Answer4 = "Joined Syntax eXtension",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Which hook is used to perform side effects in React?",
                    Answer1 = "useReducer",
                    Answer2 = "useEffect",
                    Answer3 = "useCallback",
                    Answer4 = "useRef",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is the purpose of the 'key' prop when rendering a list in React?",
                    Answer1 = "To style the element",
                    Answer2 = "To help React identify items between renders",
                    Answer3 = "To make props immutable",
                    Answer4 = "To store component state",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What does useMemo help with?",
                    Answer1 = "Running side effects",
                    Answer2 = "Memoizing expensive computed values",
                    Answer3 = "Creating global state",
                    Answer4 = "Sending HTTP requests",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is the main purpose of useCallback?",
                    Answer1 = "To memoize a function reference",
                    Answer2 = "To mutate state directly",
                    Answer3 = "To trigger a re-render",
                    Answer4 = "To fetch remote data automatically",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Which statement about props in React is correct?",
                    Answer1 = "Props are always mutable",
                    Answer2 = "Props are inputs passed from parent to child",
                    Answer3 = "Props can only be numbers",
                    Answer4 = "Props replace state completely",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is lifting state up in React?",
                    Answer1 = "Moving state to a common parent component",
                    Answer2 = "Saving state to local storage",
                    Answer3 = "Converting state to props automatically",
                    Answer4 = "Making state global by default",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What does React StrictMode do in development?",
                    Answer1 = "It disables warnings",
                    Answer2 = "It highlights potential problems by performing extra checks",
                    Answer3 = "It improves production performance automatically",
                    Answer4 = "It replaces TypeScript",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is a controlled component in React?",
                    Answer1 = "A component fully driven by React state",
                    Answer2 = "A component without props",
                    Answer3 = "A component managed by CSS only",
                    Answer4 = "A component rendered only once",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "Why can unnecessary re-renders be a problem in React?",
                    Answer1 = "They can reduce performance",
                    Answer2 = "They always cause memory leaks",
                    Answer3 = "They make JSX invalid",
                    Answer4 = "They disable hooks",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },
                new() {
                    Text = "What is the main rule about calling hooks in React?",
                    Answer1 = "Hooks can be called inside loops freely",
                    Answer2 = "Hooks should be called at the top level of React function components or custom hooks",
                    Answer3 = "Hooks can only be called in class components",
                    Answer4 = "Hooks must be called after return",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[5].Id,
                    Technology = techArray[5]
                },

                // English Grammar — techArray[6]

                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "Had I knew about the issue, I would have acted sooner.",
                    Answer2 = "Had I known about the issue, I would have acted sooner.",
                    Answer3 = "Had I know about the issue, I would have acted sooner.",
                    Answer4 = "Had I was knowing about the issue, I would have acted sooner.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is grammatically correct?",
                    Answer1 = "Neither the manager nor the employees was available.",
                    Answer2 = "Neither the manager nor the employees were available.",
                    Answer3 = "Neither the manager or the employees were available.",
                    Answer4 = "Neither manager nor the employees was available.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "The report, along with the notes, were sent yesterday.",
                    Answer2 = "The report, along with the notes, was sent yesterday.",
                    Answer3 = "The report, along with the notes, have been sent yesterday.",
                    Answer4 = "The report, along with the notes, are sent yesterday.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which option correctly completes the sentence: 'No sooner ___ the meeting started than the fire alarm went off.'",
                    Answer1 = "did",
                    Answer2 = "had",
                    Answer3 = "was",
                    Answer4 = "has",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "She is one of the best engineers who has worked here.",
                    Answer2 = "She is one of the best engineers who have worked here.",
                    Answer3 = "She is one of the best engineers which have worked here.",
                    Answer4 = "She is one of the best engineers whom has worked here.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence uses the subjunctive mood correctly?",
                    Answer1 = "I suggest that he goes now.",
                    Answer2 = "I suggest that he go now.",
                    Answer3 = "I suggest that he went now.",
                    Answer4 = "I suggest that he is going now.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "If I would have seen it, I would have told you.",
                    Answer2 = "If I had seen it, I would have told you.",
                    Answer3 = "If I seen it, I would have told you.",
                    Answer4 = "If I have saw it, I would have told you.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "He explained me the problem in detail.",
                    Answer2 = "He explained the problem to me in detail.",
                    Answer3 = "He explained to me the problem in details.",
                    Answer4 = "He explained me about the problem in detail.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "The number of applicants are increasing.",
                    Answer2 = "The number of applicants is increasing.",
                    Answer3 = "The number of applicants have increased.",
                    Answer4 = "The number of applicants increase.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is grammatically correct?",
                    Answer1 = "Scarcely had he entered when everyone stood up.",
                    Answer2 = "Scarcely he had entered when everyone stood up.",
                    Answer3 = "Scarcely had entered he when everyone stood up.",
                    Answer4 = "Scarcely he entered when everyone had stood up.",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "She denied to take the money.",
                    Answer2 = "She denied taking the money.",
                    Answer3 = "She denied take the money.",
                    Answer4 = "She denied that take the money.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "Not only he missed the deadline, but he also ignored the messages.",
                    Answer2 = "Not only did he miss the deadline, but he also ignored the messages.",
                    Answer3 = "Not only he did miss the deadline, but also he ignored the messages.",
                    Answer4 = "Not only missed he the deadline, but he also ignored the messages.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct option: 'I would rather you ___ me earlier.'",
                    Answer1 = "told",
                    Answer2 = "tell",
                    Answer3 = "have told",
                    Answer4 = "to tell",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "He is capable to solve the issue.",
                    Answer2 = "He is capable of solving the issue.",
                    Answer3 = "He is capable in solving the issue.",
                    Answer4 = "He is capable for solving the issue.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "The proposal needs revised before submission.",
                    Answer2 = "The proposal needs to be revised before submission.",
                    Answer3 = "The proposal needs revise before submission.",
                    Answer4 = "The proposal needs revising before submit.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "Every one of the solutions have drawbacks.",
                    Answer2 = "Every one of the solutions has drawbacks.",
                    Answer3 = "Every one of the solution have drawbacks.",
                    Answer4 = "Every one of solution has drawbacks.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "It was such useful advice that I wrote it down immediately.",
                    Answer2 = "It was so useful advice that I wrote it down immediately.",
                    Answer3 = "It was such a useful advices that I wrote it down immediately.",
                    Answer4 = "It was so an useful advice that I wrote it down immediately.",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which option correctly completes the sentence: 'By next month, she ___ here for ten years.'",
                    Answer1 = "will work",
                    Answer2 = "will have been working",
                    Answer3 = "has worked",
                    Answer4 = "is working",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "Rarely we see such dedication in junior candidates.",
                    Answer2 = "Rarely do we see such dedication in junior candidates.",
                    Answer3 = "Rarely we do see such dedication in junior candidates.",
                    Answer4 = "Rarely see we such dedication in junior candidates.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "Having finished the task, the report was sent.",
                    Answer2 = "Having finished the task, she sent the report.",
                    Answer3 = "Having the task finished, the report she sent.",
                    Answer4 = "Having been finished the task, she sent the report.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "She has went to the store.",
                    Answer2 = "She has gone to the store.",
                    Answer3 = "She have gone to the store.",
                    Answer4 = "She gone to the store.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence uses the Present Continuous tense?",
                    Answer1 = "They play football.",
                    Answer2 = "They played football.",
                    Answer3 = "They are playing football.",
                    Answer4 = "They have played football.",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct plural form.",
                    Answer1 = "mans",
                    Answer2 = "men",
                    Answer3 = "mens",
                    Answer4 = "manes",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which word is a noun?",
                    Answer1 = "Quickly",
                    Answer2 = "Happiness",
                    Answer3 = "Strong",
                    Answer4 = "Running",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "I didn't saw him.",
                    Answer2 = "I didn't see him.",
                    Answer3 = "I not see him.",
                    Answer4 = "I didn't seen him.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "Much people came to the meeting.",
                    Answer2 = "Many people came to the meeting.",
                    Answer3 = "Many person came to the meeting.",
                    Answer4 = "Much persons came to the meeting.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct article: 'He is ___ honest man.'",
                    Answer1 = "a",
                    Answer2 = "an",
                    Answer3 = "the",
                    Answer4 = "no article",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "She can sings very well.",
                    Answer2 = "She can to sing very well.",
                    Answer3 = "She can sing very well.",
                    Answer4 = "She cans sing very well.",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct form: 'I am interested ___ learning English.'",
                    Answer1 = "on",
                    Answer2 = "at",
                    Answer3 = "in",
                    Answer4 = "to",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "What is the superlative form of 'small'?",
                    Answer1 = "smaller",
                    Answer2 = "smallest",
                    Answer3 = "most small",
                    Answer4 = "more small",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "There isn't some milk in the fridge.",
                    Answer2 = "There isn't any milk in the fridge.",
                    Answer3 = "There aren't any milk in the fridge.",
                    Answer4 = "There isn't no milk in the fridge.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "My brother is taller than me.",
                    Answer2 = "My brother is more taller than me.",
                    Answer3 = "My brother taller than me.",
                    Answer4 = "My brother is tallest than me.",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct reported speech.",
                    Answer1 = "He said that he is tired.",
                    Answer2 = "He said that he was tired.",
                    Answer3 = "He said that was tired.",
                    Answer4 = "He said he tired.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence uses a modal verb correctly?",
                    Answer1 = "You must to finish your work.",
                    Answer2 = "You must finish your work.",
                    Answer3 = "You must finishing your work.",
                    Answer4 = "You must finished your work.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct form: 'By the time we arrived, the film ___.'",
                    Answer1 = "started",
                    Answer2 = "has started",
                    Answer3 = "had started",
                    Answer4 = "was start",
                    CorrectAnswerNumber = 3,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "Each of the students have a book.",
                    Answer2 = "Each of the students has a book.",
                    Answer3 = "Each of the student have a book.",
                    Answer4 = "Each students has a book.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "If it rains, we will stay home.",
                    Answer2 = "If it will rain, we will stay home.",
                    Answer3 = "If it rains, we would stay home.",
                    Answer4 = "If it rained, we will stay home.",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which word is a conjunction?",
                    Answer1 = "Because",
                    Answer2 = "Beautiful",
                    Answer3 = "Slowly",
                    Answer4 = "Table",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "I look forward to meet you.",
                    Answer2 = "I look forward to meeting you.",
                    Answer3 = "I look forward meet you.",
                    Answer4 = "I look forward for meeting you.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is grammatically correct?",
                    Answer1 = "Hardly had I arrived when it started to rain.",
                    Answer2 = "Hardly I had arrived when it started to rain.",
                    Answer3 = "Hardly had arrived I when it started to rain.",
                    Answer4 = "Hardly I arrived when it started to rain.",
                    CorrectAnswerNumber = 1,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is grammatically correct?",
                    Answer1 = "She don't like coffee.",
                    Answer2 = "She doesn't like coffee.",
                    Answer3 = "She not like coffee.",
                    Answer4 = "She isn't like coffee.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which tense is used in the sentence: 'I have finished my work'?",
                    Answer1 = "Past Simple",
                    Answer2 = "Present Perfect",
                    Answer3 = "Future Simple",
                    Answer4 = "Past Continuous",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which word is an adjective?",
                    Answer1 = "Run",
                    Answer2 = "Quickly",
                    Answer3 = "Beautiful",
                    Answer4 = "Running",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "He go to work every day.",
                    Answer2 = "He goes to work every day.",
                    Answer3 = "He going to work every day.",
                    Answer4 = "He gone to work every day.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which word is a pronoun?",
                    Answer1 = "House",
                    Answer2 = "Quickly",
                    Answer3 = "They",
                    Answer4 = "Blue",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence uses the Past Simple tense?",
                    Answer1 = "I am eating dinner.",
                    Answer2 = "I ate dinner.",
                    Answer3 = "I have eaten dinner.",
                    Answer4 = "I will eat dinner.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "What is the plural of 'child'?",
                    Answer1 = "childs",
                    Answer2 = "children",
                    Answer3 = "childes",
                    Answer4 = "childrens",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct article: '___ apple a day keeps the doctor away.'",
                    Answer1 = "A",
                    Answer2 = "An",
                    Answer3 = "The",
                    Answer4 = "No article",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is in the Future Simple tense?",
                    Answer1 = "She is reading.",
                    Answer2 = "She will read.",
                    Answer3 = "She has read.",
                    Answer4 = "She read.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which word is a preposition?",
                    Answer1 = "Under",
                    Answer2 = "Happy",
                    Answer3 = "Run",
                    Answer4 = "Slowly",
                    CorrectAnswerNumber = 1,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct form: 'If I ___ time, I will help you.'",
                    Answer1 = "have",
                    Answer2 = "had",
                    Answer3 = "will have",
                    Answer4 = "having",
                    CorrectAnswerNumber = 1,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "There is many books on the table.",
                    Answer2 = "There are many books on the table.",
                    Answer3 = "There be many books on the table.",
                    Answer4 = "There was many books on the table.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "What is the comparative form of 'good'?",
                    Answer1 = "gooder",
                    Answer2 = "more good",
                    Answer3 = "better",
                    Answer4 = "best",
                    CorrectAnswerNumber = 3,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "I have lived here since five years.",
                    Answer2 = "I have lived here for five years.",
                    Answer3 = "I lived here since five years.",
                    Answer4 = "I am living here since five years.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is in the passive voice?",
                    Answer1 = "The chef cooked dinner.",
                    Answer2 = "Dinner was cooked by the chef.",
                    Answer3 = "The chef is cooking dinner.",
                    Answer4 = "The chef cooks dinner.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct form: 'She suggested ___ earlier.'",
                    Answer1 = "to leave",
                    Answer2 = "leave",
                    Answer3 = "leaving",
                    Answer4 = "left",
                    CorrectAnswerNumber = 3,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which word is an adverb?",
                    Answer1 = "Careful",
                    Answer2 = "Carefully",
                    Answer3 = "Care",
                    Answer4 = "Caring",
                    CorrectAnswerNumber = 2,
                    Difficulty = 1,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct sentence.",
                    Answer1 = "Neither of the answers are correct.",
                    Answer2 = "Neither of the answers is correct.",
                    Answer3 = "Neither of the answer is correct.",
                    Answer4 = "Neither answer are correct.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Which sentence is correct?",
                    Answer1 = "He asked me where was I going.",
                    Answer2 = "He asked me where I was going.",
                    Answer3 = "He asked me where am I going.",
                    Answer4 = "He asked me where going I was.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 2,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
                new() {
                    Text = "Choose the correct conditional sentence.",
                    Answer1 = "If I would know, I would tell you.",
                    Answer2 = "If I knew, I would tell you.",
                    Answer3 = "If I know, I would tell you.",
                    Answer4 = "If I had knew, I would tell you.",
                    CorrectAnswerNumber = 2,
                    Difficulty = 3,
                    TechnologyId = techArray[6].Id,
                    Technology = techArray[6]
                },
            };

            ctx.Questions.AddRange(questions);
            shouldWeSubmit = true;
        }

        if (!ctx.Tests.Any())
        {
            List<Test> tests = new List<Test>();

            tests.Add(new Test
            {
                FinalScore = 88.5f,
                FinishDate = DateTime.Now.AddHours(-234),
                StartDate = DateTime.Now.AddHours(-234),
                Technology = techArray[0],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 34.2f,
                FinishDate = DateTime.Now.AddHours(-101),
                StartDate = DateTime.Now.AddHours(-101),
                Technology = techArray[0],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 49.0f,
                FinishDate = DateTime.Now.AddHours(-76),
                StartDate = DateTime.Now.AddHours(-76),
                Technology = techArray[0],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 99.45f,
                FinishDate = DateTime.Now.AddHours(-34),
                StartDate = DateTime.Now.AddHours(-34),
                Technology = techArray[0],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 65.5f,
                FinishDate = DateTime.Now.AddHours(-2004),
                StartDate = DateTime.Now.AddHours(-2004),
                Technology = techArray[1],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 88.5f,
                FinishDate = DateTime.Now.AddHours(-349),
                StartDate = DateTime.Now.AddHours(-349),
                Technology = techArray[1],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 17.0f,
                FinishDate = DateTime.Now.AddHours(-16),
                StartDate = DateTime.Now.AddHours(-16),
                Technology = techArray[2],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 88.5f,
                FinishDate = DateTime.Now.AddHours(-700),
                StartDate = DateTime.Now.AddHours(-700),
                Technology = techArray[2],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 91.4f,
                FinishDate = DateTime.Now.AddHours(-435),
                StartDate = DateTime.Now.AddHours(-435),
                Technology = techArray[2],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 76.5f,
                FinishDate = DateTime.Now.AddHours(-555),
                StartDate = DateTime.Now.AddHours(-555),
                Technology = techArray[2],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 4.9f,
                FinishDate = DateTime.Now.AddHours(-198),
                StartDate = DateTime.Now.AddHours(-198),
                Technology = techArray[2],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 0.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 100.0f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 2.3f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            tests.Add(new Test
            {
                FinalScore = 6.6f,
                FinishDate = DateTime.Now.AddHours(-56),
                StartDate = DateTime.Now.AddHours(-56),
                Technology = techArray[4],
                Username = "testUser"
            });

            ctx.Tests.AddRange(tests);
            shouldWeSubmit = true;
        }

        if (shouldWeSubmit)
        {
            ctx.SaveChanges();
        }
    }
}
