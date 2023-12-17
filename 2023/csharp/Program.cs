string output;

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
        "04" => 
            r3im.aof.d04.Task1.run() + "\n" +
            r3im.aof.d04.Task2.run(),
        "05" => 
            r3im.aof.d05.Task.run(p: args[1], f: args[2]),
        "06" => 
            r3im.aof.d06.Task1.run() + "\n" +
            r3im.aof.d06.Task2.run(),
        "07" => 
            r3im.aof.d07.Task.run(p: args[1], f: args[2]),
        "08" => 
            r3im.aof.d08.Task.run(p: args[1], f: args[2]),
        "09" => 
            r3im.aof.d09.Task.run(p: args[1], f: args[2]),
        _ => $"{args[0]} not implemented",
    };
}

Console.WriteLine(output);

