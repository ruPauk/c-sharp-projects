using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        const double GRAVITY = -3.711;
        Landscape landscape = new Landscape();
        string[] inputs;
        int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
        for (int i = 0; i < surfaceN; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
            int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            //парсинг landscape
            landscape.AddDot(landX, landY);
        }
        landscape.FindLandToPlace();
        double realVerticalSpeed = 0;
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);
            int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            int power = int.Parse(inputs[6]); // the thrust power (0 to 4).
            
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            realVerticalSpeed = realVerticalSpeed + GRAVITY + power;
            int height = Player.GetHeight(Y, landscape.y1);
            int hx = Y;
            int vs = vSpeed;
            Console.Error.WriteLine("VS = " + vs.ToString());
            int thrust = 0;
            for (int i = 0; i < height; i++) {
                vs += (int)Math.Round(GRAVITY, 0) + thrust;
                Console.Error.WriteLine("mVS = " + vs.ToString() + "   " + Math.Round(GRAVITY, 0).ToString());
                hx += vs;
                if (thrust<4)
                {
                    ++thrust;
                }
                if (hx < landscape.y1)
                {
                    break;
                }
            }
            if (vs <= -40)
            {
                Console.WriteLine("0 4");
            }
            else
            {
                Console.WriteLine("0 0");
            }
            // 2 integers: rotate power. rotate is the desired rotation angle (should be 0 for level 1), power is the desired thrust power (0 to 4).
            
        }
    }
    /*public static void NormalizeSpeed(int vSpeed, int Y ) {
            //Math.Round(2.5, MidpointRounding.AwayFromZero)
        
        int

    }*/

    public static int GetHeight(int YShip, int YLand) {
        return (YShip - YLand);
    }
}

public class Landscape {
    public List<int> x;
    public List<int> y;
    public int x1, y1, x2, y2; //координаты посадочной полосы
    public Landscape() {
        this.x = new List<int>();
        this.y = new List<int>();
        this.x1 = 0;
        this.y1 = 0;
        this.x2 = 0;
        this.y2 = 0;
    }
    public bool AddDot(int x, int y) {
        this.x.Add(x);
        this.y.Add(y);
        return true;
    }
    public bool FindLandToPlace() {
        for(int i = 0; i<this.y.Count-1; i++) {
            if ((this.y[i]==this.y[i+1]) && ((this.x[i+1]-this.x[i])>=1000)) {
                this.x1 = this.x[i];
                this.y1 = this.y[i];
                this.x2 = this.x[i+1];
                this.x2 = this.x[i+1];
                return true;
            }
        }
        return false;
    }
}


