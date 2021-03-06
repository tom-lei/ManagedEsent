# C++ PersistentDictionary Sample

Here is an application that remembers a first name -> last name mapping in a persistent dictionary.

```c++
using namespace System;
using namespace Microsoft::Isam::Esent::Collections::Generic;

int main(array<System::String ^> ^)
{
    PersistentDictionary<String ^, String ^> ^ dictionary = gcnew PersistentDictionary<String ^, String ^>(L"Names");
    Console::WriteLine("What is your first name?");
    String ^firstName = Console::ReadLine();
    if (dictionary->ContainsKey(firstName))
    {
        Console::WriteLine("Welcome back {0} {1}", firstName, dictionary[firstName](firstName));
    }
    else
    {
        Console::WriteLine("I don't know you, {0}. What is your last name?", firstName);
        dictionary[firstName](firstName) = Console::ReadLine();
    }

    return 0;
}
```
