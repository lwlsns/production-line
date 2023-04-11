using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLine
{
    public class ConveyorBelt
    {
        private char?[] conveyorBelt;
        private bool[] beltSlotInteractable;
        private int beltLength;
        private IRandomGenerator randomGenerator;

        public ConveyorBelt(int beltLength, IRandomGenerator randomGenerator)
        {
            this.beltLength = beltLength;
            conveyorBelt = new char?[beltLength];
            beltSlotInteractable = new bool[beltLength];
            this.randomGenerator = randomGenerator;
        }

        public char?[] GetBelt()
        {
            return conveyorBelt;
        }

        public int Length
        {
            get { return beltLength; }
        }

        //check to see if the specified slot is interactable and empty
        public bool CanPutItem(int slot)
        {
            return beltSlotInteractable[slot] && conveyorBelt[slot] == null;
        }

        //put component or product in the specified slot
        public void PutItem(int slot, char? item)
        {
            conveyorBelt[slot] = item;
            beltSlotInteractable[slot] = false;
        }

        //peek at the component in the specified slot
        public char? PeekComponent(int slot)
        {
            return conveyorBelt[slot];
        }

        //check to see if the specified slot is interactable and has a component
        public bool CanGetComponent(int slot)
        {
            return beltSlotInteractable[slot] && conveyorBelt[slot] != null;
        }

        //get the component in the specified slot and set the slot to not interactable
        public char? GetComponent(int slot)
        {
            char? component = conveyorBelt[slot];
            conveyorBelt[slot] = null;
            beltSlotInteractable[slot] = false;
            return component;
        }

        //move the belt one place to the right and set all slots to interactable
        public void MoveBelt()
        {
            for (int i = conveyorBelt.Length - 1; i > 0; i--)
            {
                conveyorBelt[i] = conveyorBelt[i - 1];
                beltSlotInteractable[i] = true;
            }
            conveyorBelt[0] = GenerateComponent();
            beltSlotInteractable[0] = true;
        }

        //Generate a new component with equal chance of 'A', 'B', or Null (no component)
        private char? GenerateComponent()
        {
            switch (randomGenerator.Next(0, 3))
            {
                case 0:
                    return 'A';
                case 1:
                    return 'B';
                case 2:
                    return null;
                default:
                    return null;
            }
        }

    }
}
