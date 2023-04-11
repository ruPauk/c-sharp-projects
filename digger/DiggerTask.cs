using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            //throw new NotImplementedException();
            var resAct = new CreatureCommand();
            resAct.DeltaX = resAct.DeltaY = 0;
            resAct.TransformTo = this;
            return resAct;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    class Player : ICreature
    {
        static string sprite = "Digger.png";
        static int drawPriority = 100;

        public CreatureCommand Act(int x, int y)
        {
            var creatureCommand = new CreatureCommand();
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Left:
                case System.Windows.Forms.Keys.L:
                    if (IsPossibleToStep(x, y, -1, 0))
                        return GetCreatureCommand(creatureCommand, -1, 0);
                    break;
                case System.Windows.Forms.Keys.Right:
                case System.Windows.Forms.Keys.R:
                    if (IsPossibleToStep(x, y, 1, 0))
                        return GetCreatureCommand(creatureCommand, 1, 0);
                    break;
                case System.Windows.Forms.Keys.Up:
                case System.Windows.Forms.Keys.U:
                    if (IsPossibleToStep(x, y, 0, -1))
                        return GetCreatureCommand(creatureCommand, 0, -1);
                    break;
                case System.Windows.Forms.Keys.Down:
                case System.Windows.Forms.Keys.D:
                    if (IsPossibleToStep(x, y, 0, 1))
                        return GetCreatureCommand(creatureCommand, 0, 1);
                    break;
            }
            creatureCommand = GetCreatureCommand(creatureCommand, 0, 0);
            return creatureCommand;
        }

        public bool IsPossibleToStep(int currentX, int currentY, int deltaX, int deltaY)
        {
            if (currentX + deltaX < 0 || currentX + deltaX > Game.Map.GetUpperBound(0))
                return false;
            return !(currentY + deltaY < 0 || currentY + deltaY > Game.Map.GetUpperBound(1));
        }

        public CreatureCommand GetCreatureCommand(CreatureCommand creatureCommand, int deltaX, int deltaY)
        {
            creatureCommand.DeltaX = deltaX;
            creatureCommand.DeltaY = deltaY;
            creatureCommand.TransformTo = this;
            return creatureCommand;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return drawPriority;
        }

        public string GetImageFileName()
        {
            return sprite;
        }
    }
}