/*
using System;

using System.Threading;
public class Example
{
    public static void run()
    {
        WriteCharacterStrings(1, 30, true);
        Console.MoveBufferArea(0, Console.CursorTop - 10, 30, 1, Console.CursorLeft, Console.CursorTop + 1);
        Console.CursorTop = Console.CursorTop + 3;
        Console.WriteLine("Press any key...");
        Console.ReadKey();

        Console.Clear();
        WriteCharacterStrings(1, 26, false);
    }

    private static void WriteCharacterStrings(int start, int end, bool changeColor)
    {
        for (int ctr = start; ctr <= end; ctr++)
        {
            if (changeColor)
                Console.BackgroundColor = (ConsoleColor)((ctr - 1) % 16);

            Console.WriteLine(new String(Convert.ToChar(ctr + 64), 30));
        }
    }
}

 * 
 *
 */
// MonitorSample.cs
// This example shows use of the following methods of the C# lock keyword
// and the Monitor class 
// in threads:
//      Monitor.Pulse(Object)
//      Monitor.Wait(Object)
using System;
using System.Threading;

public class MonitorSample
{

    public static void Error_Test()
    {
        int increment = 0;
        bool exitFlag = false;

        while (!exitFlag)
        {
            if (Console.IsOutputRedirected)
            {
                Console.WriteLine("\nIs OutputRedirected.");
                Console.Error.WriteLine("Generating multiples of numbers from {0} to {1}",
                                        increment + 1, increment + 10);
            }
            Console.WriteLine("Generating multiples of numbers from {0} to {1}",
                              increment + 1, increment + 10);
            for (int ctr = increment + 1; ctr <= increment + 10; ctr++)
            {
                Console.Write("Multiples of {0}: ", ctr);
                for (int ctr2 = 1; ctr2 <= 10; ctr2++)
                    Console.Write("{0}{1}", ctr * ctr2, ctr2 == 10 ? "" : ", ");

                Console.WriteLine();
            }
            Console.WriteLine();

            increment += 10;
            Console.Error.Write("Display multiples of {0} through {1} (y/n)? ",
                                increment + 1, increment + 10);
            Char response = Console.ReadKey(true).KeyChar;
            Console.Error.WriteLine(response);
            if (!Console.IsOutputRedirected)
                Console.CursorTop--;

            if (Char.ToUpperInvariant(response) == 'N')
                exitFlag = true;
        }
    }

    public static void Temp()
    {
        int result = 0;   // Result initialized to say there is no error
        Cell cell = new Cell();

        CellProd prod = new CellProd(cell, 20);  // Use cell for storage, 
                                                 // produce 20 items
        CellCons cons = new CellCons(cell, 20);  // Use cell for storage, 
                                                 // consume 20 items

        Thread producer = new Thread(new ThreadStart(prod.ThreadRun));
        Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));
        // Threads producer and consumer have been created, 
        // but not started at this point.

        try
        {
            producer.Start();
            consumer.Start();

            producer.Join();   // Join both threads with no timeout
                               // Run both until done.
            consumer.Join();
            // threads producer and consumer have finished at this point.
        }
        catch (ThreadStateException e)
        {
            Console.WriteLine(e);  // Display text of exception
            result = 1;            // Result says there was an error
        }
        catch (ThreadInterruptedException e)
        {
            Console.WriteLine(e);  // This exception means that the thread
                                   // was interrupted during a Wait
            result = 1;            // Result says there was an error
        }
        // Even though Main returns void, this provides a return code to 
        // the parent process.
        Environment.ExitCode = result;
        Console.WriteLine(result);

        Console.ReadKey();
    }
}

public class CellProd
{
    Cell cell;         // Field to hold cell object to be used
    int quantity = 1;  // Field for how many items to produce in cell

    public CellProd(Cell box, int request)
    {
        cell = box;          // Pass in what cell object to be used
        quantity = request;  // Pass in how many items to produce in cell
    }
    public void ThreadRun()
    {
        for (int looper = 1; looper <= quantity; looper++)
            cell.WriteToCell(looper);  // "producing"
    }
}

public class CellCons
{
    Cell cell;         // Field to hold cell object to be used
    int quantity = 1;  // Field for how many items to consume from cell

    public CellCons(Cell box, int request)
    {
        cell = box;          // Pass in what cell object to be used
        quantity = request;  // Pass in how many items to consume from cell
    }
    public void ThreadRun()
    {
        int valReturned;
        for (int looper = 1; looper <= quantity; looper++)
            // Consume the result by placing it in valReturned.
            valReturned = cell.ReadFromCell();
    }
}

public class Cell
{
    int cellContents;         // Cell contents
    bool readerFlag = false;  // State flag
    public int ReadFromCell()
    {
        lock (this)   // Enter synchronization block
        {
            if (!readerFlag)
            {            // Wait until Cell.WriteToCell is done producing
                try
                {
                    // Waits for the Monitor.Pulse in WriteToCell
                    Monitor.Wait(this);
                }
                catch (SynchronizationLockException e)
                {
                    Console.WriteLine(e);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine("Consume: {0}", cellContents);
            readerFlag = false;    // Reset the state flag to say consuming
                                   // is done.
            Monitor.Pulse(this);   // Pulse tells Cell.WriteToCell that
                                   // Cell.ReadFromCell is done.
        }   // Exit synchronization block
        return cellContents;
    }

    public void WriteToCell(int n)
    {
        lock (this)  // Enter synchronization block
        {
            if (readerFlag)
            {      // Wait until Cell.ReadFromCell is done consuming.
                try
                {
                    Monitor.Wait(this);   // Wait for the Monitor.Pulse in
                                          // ReadFromCell
                }
                catch (SynchronizationLockException e)
                {
                    Console.WriteLine(e);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e);
                }
            }
            cellContents = n;
            Console.WriteLine("Produce: {0}", cellContents);
            readerFlag = true;    // Reset the state flag to say producing
                                  // is done
            Monitor.Pulse(this);  // Pulse tells Cell.ReadFromCell that 
                                  // Cell.WriteToCell is done.
        }   // Exit synchronization block
    }
}
