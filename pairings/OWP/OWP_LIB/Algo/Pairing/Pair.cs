using OWP_LIB.Exceptions;
using OWP_LIB.Models;
using System;
using System.Collections.Generic;

namespace OWP_LIB.Algo.Pairing
{
    public class MatchPairing
    {
        private const int DefaultTeamSize = 8;

        public Dictionary<int, ArmyList> ListUs { get; set; }
        public Dictionary<int, ArmyList> ListThem { get; set; }
        public int[][] PairingDataUs { get; }
        public int[][] PairingDataThem { get; }
        public int SelectionUs { get; }
        public int SelectionThem { get; }
        public int TeamSize { get; } = DefaultTeamSize;
        public int PairingMatrixSize { get; } = DefaultTeamSize;

        public MatchPairing(int[][] pairingDataUs, Dictionary<int, ArmyList> listUs, Dictionary<int, ArmyList> listThem) : this (pairingDataUs, pairingDataUs, listUs, listThem){ }

        public MatchPairing(int[][] pairingDataUs, int[][] pairingDataThem, Dictionary<int, ArmyList> listUs, Dictionary<int, ArmyList> listThem) : this(pairingDataUs, pairingDataThem, listUs, listThem, 8) { }

        public MatchPairing(int[][] pairingDataUs, int[][] pairingDataThem, Dictionary<int, ArmyList> listUs, Dictionary<int, ArmyList> listThem, int teamSize)
        {
            ValidatePairingData(pairingDataUs);
            ValidatePairingData(pairingDataThem);
            ValidateLists(listUs);
            ValidateLists(listThem);
            PairingDataUs = pairingDataUs;
            PairingDataThem = pairingDataThem;
            ListUs = listUs;
            ListThem = listThem;
            SelectionUs = CreateSelectionMask(TeamSize);
            SelectionThem = CreateSelectionMask(TeamSize);
        }

        public ArmyList SelectPresenter()
        {
            ValidateRemainingArmies(4, true);

            var highest = int.MinValue;
            var index = 0;

            for (int i = 0; i < TeamSize; i++)
            {
                if ((SelectionUs & (1 << i)) != 0)
                {
                    var total = SelectCounterTotal(i, ClearBit(SelectionUs, i), SelectionThem);

                    if (total[0] > highest)
                    {
                        highest = total[0];
                        index = i;
                    }
                }
            }

            return ListUs[index];
        }

        public (ArmyList list1, ArmyList list2) SelectCounters()
        {
            ValidateRemainingArmies(3, false);

            throw new NotImplementedException();
        }

        public ArmyList ChooseCounter(ArmyList list1, ArmyList list2)
        {
            ValidateRemainingArmies(5, false);

            throw new NotImplementedException();
        }

        public List<ArmyList> ChooseCounterAndRemainnig(ArmyList list1, ArmyList list2)
        {
            ValidateRemainingArmies(3, false);
            throw new NotImplementedException();
        }

        private int[] SelectPresenterTotal(int us, int them)
        {
            var result = new int[] { int.MinValue, -1, -1 };
            
            for (int i = 0; i < TeamSize; i++)
            {
                if ((us & (1 << i)) != 0)
                {
                    var potential = SelectCounterTotal(i, ClearBit(us, i), them);
                    
                    if (potential[0] > result[0])
                    {
                        result = potential;
                    }
                }
            }

            return result;
        }

        private int[] SelectCounterTotal(int index_us, int us, int them)
        {
            var result = new int[] { int.MaxValue, index_us, -1 };

            for (int i = 0; i < TeamSize; i++)
            {
                if ((them & (1 << i)) != 0)
                {
                    for (int k = i + 1; k < TeamSize; k++)
                    {
                        if ((them & (1 << k)) != 0)
                        {
                            var potentail = ChooseCounterTotal(index_us, new int[] { i, k }, us, them);

                            if (potentail[0] < result[0])
                            {
                                result = potentail;
                            }
                        }
                    }
                }
            }

            return result;
        }

        private int[] ChooseCounterTotal(int us_index, int[] indexes_them, int us, int them)
        {
            var result = new int[] { int.MinValue, us_index, -1 };

            for (var i = 0; i < indexes_them.Length; i++)
            {
                var chosenCounter = indexes_them[i];
                var our_potential = SelectPresenterTotal(us, ClearBit(them, chosenCounter));
                our_potential[0] += ScorePairing(us_index, chosenCounter);

                if (our_potential[0] > result[0])
                {
                    result[0] = our_potential[0];
                    result[1] = us_index;
                    result[2] = chosenCounter;
                }
            }

            return result;
        }
        private int SetBit(int value, int i)
        {
            return (int)(value | (1 << i));
        }

        private int ClearBit(int value, int i)
        {
            return (int)(value & ~(1 << i));
        }

        private int ScorePairing(int usIndex, int themIndex)
        {
            return PairingDataUs[usIndex][themIndex] - PairingDataThem[themIndex][usIndex];
        }

        private int CountSetBits(int value)
        {
            var count = 0;

            while (value != 0)
            {
                count += value & 1;
                value >>= 1;
            }

            return count;
        }

        private int GetFirstSetBit(int value)
        {
            for (int i = 0; i < TeamSize; i++)
            {
                if ((value & (1 << i)) != 0)
                {
                    return i;
                }
            }

            return -1;
        }

        private int CreateSelectionMask(int bitCount)
        {
            return (1 << bitCount) - 1;
        }


        private void ValidatePairingData(int[][] pairingData)
        {
            if (pairingData is null)
                throw new BadPairingDataException("Pairing data cannot be null.");

            if (pairingData.Length != PairingMatrixSize)
                throw new BadPairingDataException($"Pairing data must contain exactly {PairingMatrixSize} rows.");

            for (var i = 0; i < pairingData.Length; i++)
            {
                if (pairingData[i] is null)
                    throw new BadPairingDataException($"Pairing data row {i} cannot be null.");

                if (pairingData[i].Length != PairingMatrixSize)
                    throw new BadPairingDataException($"Pairing data row {i} must contain exactly {PairingMatrixSize} columns.");
            }
        }

        private void ValidateLists(Dictionary<int, ArmyList> list)
        {
            if (list is null)
                throw new BadPairingDataException("Our army list collection cannot be null.");

            if (list.Count != TeamSize)
                throw new BadPairingDataException($"Our army list collection must contain exactly {TeamSize} lists.");

            foreach (var key in list.Keys)
            {
                if (key < 0 || key > TeamSize - 1)
                    throw new BadPairingDataException($"Army list index {key} must be between 0 and {TeamSize - 1}.");
            }
        }

        private void ValidateRemainingArmies(int num, bool isEven)
        {
            var countUs = CountSetBits(SelectionUs);
            var countThem = CountSetBits(SelectionThem);

            var usValid = isEven ? countUs % 2 == 0 : countUs % 2 != 0;
            var themValid = isEven ? countThem % 2 == 0 : countThem % 2 != 0;

            if (!(usValid && countUs >= num && themValid && countThem >= num))
            {
                throw new InvalidPairingStateException(
                    $"Incorrect amount of remaining armies. Us: {countUs}. Them: {countThem}."
                );
            }
        }
    }

}
