using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public enum TypeOfSudokuMatrix
    {
        Four = 4,
        Five,
        Six,
        Seven,
        Eight,
        Nine
    };

    public enum TypeOfShopElement
    {
        Pearls,
        Lives,
        Hints
    };

    public struct BuyLifes
    {
        public int count;
        public int price;

        public BuyLifes(int _count)
        {
            count = _count;
            switch (_count)
            {
                
                case 1:
                    price = 10;
                    break;
                case 3:
                    price = 100;
                    break;
                case 5:
                    price = 500;
                    break;
                default:
                    price = 1000;
                    break;
            }
        }
    };

    public struct BuyHints
    {
        public int count;
        public int price;

        public BuyHints(int _count)
        {
            count = _count;
            switch (_count)
            {
                case 1:
                    price = 10;
                    break;
                case 2:
                    price = 20;
                    break;
                case 3:
                    price = 30;
                    break;
                case 4:
                    price = 60;
                    break;
                case 5:
                    price = 100;
                    break;
                case 6:
                    price = 1000;
                    break;
                default:
                    price = 0;
                    break;
            }
        }
    };
}
