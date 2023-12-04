﻿string output;

if (args.Length == 0) {
    output = "Please select a day '01' - '24'";
} else {
    output = args[0] switch {
        "01" => 
            r3im.aof.d01.Task1.run() + "\n" +
            r3im.aof.d01.Task2.run(),
        "02" => 
            r3im.aof.d02.Task1.run() + "\n" +
            r3im.aof.d02.Task2.run(),
        "03" => 
            r3im.aof.d03.Task1.run() + "\n" +
            r3im.aof.d03.Task2.run(),
        _ => $"{args[0]} not implemented",
    };
}

Console.WriteLine(output);

