using System;
using System.Collections.Generic;

var baseClass = new BaseClass("Вася");
var slaveClass = new Class2("Вася", "Пупкин");
Console.WriteLine(baseClass == slaveClass);
baseClass = slaveClass;
Console.WriteLine(baseClass == slaveClass);
Console.WriteLine((baseClass as Class2).LastName);
var baseRecord = new BaseRecord("Петя");
var slaveRecord = new Record1("Петя");
Console.WriteLine(baseRecord == slaveRecord);
baseRecord = slaveRecord;
Console.WriteLine(baseRecord == slaveRecord);
Console.WriteLine(baseRecord.Name);
var slaveRecord2 = new Record1("Петя") { Name = "Вася"};
baseRecord = slaveRecord2;
Console.WriteLine(baseRecord.Name);




public interface IBase
{
    public string Name { get; init; }
}

public class BaseClass : IBase
{
    public string Name {get; init; }

    public BaseClass(string name) => Name = name;
}

public class Class2 : BaseClass
{
    public string LastName {get;}

    public Class2(string name, string last) : base(name) => LastName = last;
}

public record BaseRecord : IBase
{
    public string Name { get; init; }

    public BaseRecord(string name) => Name = name;
}

public record Record1(string Name) : BaseRecord(Name);

public record Record2 : BaseRecord
{
    public Record2(string name) : base(name) { }
}