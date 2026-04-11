using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWP_LIB.Algo.Pairing;
using OWP_LIB.Enum;
using OWP_LIB.Models;
using System;
using System.Collections.Generic;

namespace OWP_LIB_tests.Algo.Pairing_tests
{
    [TestClass]
    public class MatchPairing_tests
    {
        private static IEnumerable<object[]> invalid_pairing_data_cases()
        {
            yield return new object[]
            {
                null!,
                "Pairing data cannot be null."
            };

            yield return new object[]
            {
                new int[7][],
                "Pairing data must contain exactly 8 rows."
            };

            var nullRowMatrix = CreateMatrix();
            nullRowMatrix[3] = null!;
            yield return new object[]
            {
                nullRowMatrix,
                "Pairing data row 3 cannot be null."
            };

            var badColumnMatrix = CreateMatrix();
            badColumnMatrix[4] = new int[7];
            yield return new object[]
            {
                badColumnMatrix,
                "Pairing data row 4 must contain exactly 8 columns."
            };
        }

        private static IEnumerable<object[]> invalid_list_cases()
        {
            yield return new object[]
            {
                null!,
                "Our army list collection cannot be null."
            };

            var tooSmall = CreateLists();
            tooSmall.Remove(7);
            yield return new object[]
            {
                tooSmall,
                "Our army list collection must contain exactly 8 lists."
            };

            var badKey = CreateLists();
            var moved = badKey[7];
            badKey.Remove(7);
            badKey.Add(8, moved);
            yield return new object[]
            {
                badKey,
                "Army list index 8 must be between 0 and 7."
            };
        }

        [TestMethod]
        public void MatchPairing_constructor_valid_input_sets_pairing_data()
        {
            var pairingDataUs = CreateMatrix();
            var pairingDataThem = CreateMatrix();
            var listUs = CreateLists();
            var listThem = CreateLists();

            var sut = new MatchPairing(pairingDataUs, pairingDataThem, listUs, listThem);

            Assert.AreSame(pairingDataUs, sut.PairingDataUs);
            Assert.AreSame(pairingDataThem, sut.PairingDataThem);
            Assert.AreSame(listUs, sut.ListUs);
            Assert.AreSame(listThem, sut.ListThem);
            Assert.AreEqual(8, sut.TeamSize);
            Assert.AreEqual(8, sut.PairingMatrixSize);
        }

        [DataTestMethod]
        [DynamicData(nameof(invalid_pairing_data_cases), DynamicDataSourceType.Method)]
        public void MatchPairing_constructor_rejects_bad_pairing_data(int[][] pairingDataUs, string expectedMessage)
        {
            var validMatrix = CreateMatrix();
            var listUs = CreateLists();
            var listThem = CreateLists();

            var ex = Assert.Throws<Exception>(() => new MatchPairing(pairingDataUs, validMatrix, listUs, listThem));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [DataTestMethod]
        [DynamicData(nameof(invalid_list_cases), DynamicDataSourceType.Method)]
        public void MatchPairing_constructor_rejects_bad_us_lists(Dictionary<int, ArmyList> listUs, string expectedMessage)
        {
            var validMatrix = CreateMatrix();
            var listThem = CreateLists();

            var ex = Assert.Throws<Exception>(() => new MatchPairing(validMatrix, validMatrix, listUs, listThem));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestMethod]
        public void MatchPairing_single_matrix_overload_uses_same_matrix_for_both_sides()
        {
            var pairingData = CreateMatrix();
            var listUs = CreateLists();
            var listThem = CreateLists();

            var sut = new MatchPairing(pairingData, listUs, listThem);

            Assert.AreSame(pairingData, sut.PairingDataUs);
            Assert.AreSame(pairingData, sut.PairingDataThem);
        }

        [TestMethod]
        public void SelectPresenter_returns_list_with_best_forced_pairing_outcome()
        {
            var pairingDataUs = CreateMatrix();
            var pairingDataThem = CreateMatrix();
            pairingDataUs[0][0] = 10;
            pairingDataUs[0][1] = 10;
            pairingDataUs[0][2] = 10;
            pairingDataUs[0][3] = 10;
            pairingDataUs[0][4] = 10;
            pairingDataUs[0][5] = 10;
            pairingDataUs[0][6] = 10;
            pairingDataUs[0][7] = 10;

            var listUs = CreateLists();
            var listThem = CreateLists();
            var expected = listUs[0];

            var sut = new MatchPairing(pairingDataUs, pairingDataThem, listUs, listThem);

            var actual = sut.SelectPresenter();

            Assert.AreSame(expected, actual);
        }

        private static int[][] CreateMatrix()
        {
            var matrix = new int[8][];

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new int[8];
            }

            return matrix;
        }

        private static Dictionary<int, ArmyList> CreateLists()
        {
            var result = new Dictionary<int, ArmyList>();

            for (int i = 0; i < 8; i++)
            {
                result[i] = new ArmyList
                {
                    Faction = (Faction)(i % Enum.GetValues(typeof(Faction)).Length),
                    PlayerGuid = Guid.NewGuid()
                };
            }

            return result;
        }
    }
}
