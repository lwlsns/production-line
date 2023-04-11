using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLine
{
    public class Worker
    {
        private int slotPosition; 
        private char?[] hands = new char?[2];
        private ConveyorBelt belt;
        private int assemblyTimeRemaining = -1;
        private int workerNum;
        private int assemblyDuration = 4;

        public Worker(ConveyorBelt belt, int slotPosition, int workerNum)
        {
            this.slotPosition = slotPosition;
            this.belt = belt;
            this.workerNum = workerNum;
        }

        public char?[] Hands
        {
            get { return hands; }
        }

        public void Update()
        {
            PickUpComponent();
            AssembleProduct();
            PlaceProduct();
        }

        //check to see if the worker can pick up a component
        public bool CanPickUpComponent()
        {
            return belt.CanGetComponent(slotPosition) && 
                hands.Contains(null) && 
                !hands.Contains(belt.PeekComponent(slotPosition)) &&
                belt.PeekComponent(slotPosition) != 'P';
        }

        public void PickUpComponent()
        {
            if (CanPickUpComponent())
            {
                char? component = belt.GetComponent(slotPosition);
                if (hands[0] == null)
                {
                    hands[0] = component;
                }
                else
                {
                    hands[1] = component;
                }
            }
        }

        //check to see if the worker can assemble a product with parts 'A' and 'B'
        public bool CanAssembleProduct()
        {
            return hands.Contains('A') && hands.Contains('B') && assemblyTimeRemaining == -1;
        }

        public void AssembleProduct()
        {
            if (assemblyTimeRemaining > 0)
            {
                assemblyTimeRemaining--;
                if (assemblyTimeRemaining == 0)
                {
                    hands[0] = 'P';
                    hands[1] = null;
                }
            }
            else if (CanAssembleProduct())
            {
                assemblyTimeRemaining = assemblyDuration;
            }
            
        }

        public bool CanPlaceProduct()
        {
            return assemblyTimeRemaining == 0 && belt.CanPutItem(slotPosition);
        }

        public void PlaceProduct()
        {
            if (CanPlaceProduct())
            {
                assemblyTimeRemaining = -1;
                hands[0] = null;
                hands[1] = null;
                belt.PutItem(slotPosition, 'P');
            }
        }

        public void PrintHands()
        {
            Console.Write("{0}, ", hands[0] ?? ' ');
            Console.Write("{0},", hands[1] ?? ' ');
            if (assemblyTimeRemaining > -1)
            {
                Console.Write(" {0}", assemblyTimeRemaining);
            }
            else
            {
                Console.Write("  ");
            }
            
        }
    }
}
