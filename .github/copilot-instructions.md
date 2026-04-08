# Project Overview

*CalendrierRepublicain* is a .NET library for converting between Gregorian and French Republican calendar dates. It is published as a NuGet package and designed to be used in other .NET applications.

## Folder Structure

- `/src`: Contains the source code of the application.
- `/test`: Contains unit tests for the application, using the same folder structure as the source code.
- `/assets`: Contains images and other assets used in the application.
- `/.github`: Contains flow and configuration files for GitHub.

## Libraries and Frameworks

- Use .NET in version 10.0.x as basic framework.

## Coding Standards

- Follow Microsoft's C# coding standards.
- Use the file-scoped namespace syntax for namespaces.
- Always use the type name instead of the C# keyword for static members and extension methods. For example, use `String.Empty` instead of `string.Empty`, and `Int32.TryParse` instead of `int.TryParse`. Use the C# keyword for variable declarations and type casts.
- Create a separate file for each class, interface, or enum. Exception: non-generic and generic interfaces can be in the same file.
- Add <summary> and (where applicable) <param> and <return> comments to every class, interface and all their local and public members. Put longer explanations and remarks in a <remarks> section.
- Always use English for code and comments.
- Leave two blank lines between the using statements and the namespace declaration, and between methods and properties.
- Sort order of using directives: 1) System namespace, 2) Microsoft namespace, 3) Solution namespace, 4) External Libraries.
- Avoid antipatterns like services locator, magic strings, and God objects.
- Always use the least permissive access modifier for classes and class members. Use `public` only for in classes and members that need to be accessed from outside the assembly, or if required by the framework (e.g., for dependency injection).

## Testing

- Use MsTest and Moq when writing test code.
- Have one test class per class in the source code.
- Have one or more test methods per public method in the source code.
- Naming convention for test methods: MethodName_StateUnderTest_ExpectedBehavior.

## Commit messages

- Always write commit messages in English.
- Use present tense, imperative mood, and be concise.
- Be very short, ideally 50 characters or less. Don't comment on every small change, but group related changes into one short sentence.

# General behavior

- Communicate to the developer at the prompt in a friendly, appreciative, and cooperative tone.
- Talk informal German to GitHub user sinistrius, but remember to use English for code, comments, and commit messages.
- Always ask if a prompt is unclear. Don't make assumptions.
- Point out disadvantages of your suggestions, if any.
- When proposing code changes, don't show the whole file, only the relevant parts.
