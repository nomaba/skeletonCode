//Skeleton Program code for the AQA A Level Paper 1 Summer 2026 examination
//this code should be used in conjunction with the Preliminary Material
//written by the AQA Programmer Team
//Modified by the ZigZag Programmer Team
//developed in the Visual Studio Community Edition programming environment
//Version 2

using System;
using System.Collections.Generic;
using System.Linq;

namespace AntSimCS              //Q13
{
    class Program
    {
        public static Random RGen = new Random();

        static void Main()
        {
            List<int> SimulationParameters = new List<int>();
            //CHANGE
            Console.WriteLine("Enter in the starting amount of food units in food cells: ");
            int StartingAmountOfFoodInFoodCells = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter simulation number: ");
            string SimNo = Console.ReadLine();
            switch (SimNo)
            {
                case "1":
                    SimulationParameters = new List<int> { 1, 5, 5, 500, 3, 5, 1000, 50, StartingAmountOfFoodInFoodCells };
                    break;
                case "2":
                    SimulationParameters = new List<int> { 1, 5, 5, 500, 3, 5, 1000, 100, StartingAmountOfFoodInFoodCells };
                    break;
                case "3":
                    SimulationParameters = new List<int> { 1, 10, 10, 500, 3, 9, 1000, 25, StartingAmountOfFoodInFoodCells };
                    break;
                case "4":
                    SimulationParameters = new List<int> { 2, 10, 10, 500, 3, 6, 1000, 25, StartingAmountOfFoodInFoodCells };
                    break;
            }
            Simulation ThisSimulation = new Simulation(SimulationParameters);
            string Choice;
            //END CHANGE
            do
            {
                DisplayMenu();
                Choice = GetChoice();
                switch (Choice)
                {
                    case "1":
                        Console.WriteLine(ThisSimulation.GetDetails());
                        break;
                    case "2":
                        int StartRow = 0, StartColumn = 0, EndRow = 0, EndColumn = 0;
                        GetCellReference(ref StartRow, ref StartColumn);
                        GetCellReference(ref EndRow, ref EndColumn);
                        Console.WriteLine(ThisSimulation.GetAreaDetails(StartRow, StartColumn, EndRow, EndColumn));
                        break;
                    case "3":
                        int Row = 0, Column = 0;
                        GetCellReference(ref Row, ref Column);
                        Console.WriteLine(ThisSimulation.GetCellDetails(Row, Column));
                        break;
                    case "4":
                        ThisSimulation.AdvanceStage(1);
                        Console.WriteLine($"Simulation moved on one stage{Environment.NewLine}");
                        break;
                    case "5":
                        Console.Write("Enter number of stages to advance by: ");
                        int NumberOfStages = Convert.ToInt32(Console.ReadLine());
                        ThisSimulation.AdvanceStage(NumberOfStages);
                        Console.WriteLine($"Simulation moved on {NumberOfStages} stages{Environment.NewLine}");
                        break;
                }
            } while (Choice != "9");
            Console.ReadLine();
        }

        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Display overall details");
            Console.WriteLine("2. Display area details");
            Console.WriteLine("3. Inspect cell");
            Console.WriteLine("4. Advance one stage");
            Console.WriteLine("5. Advance X stages");
            Console.WriteLine("9. Quit");
            Console.WriteLine();
            Console.Write("> ");
        }

        static string GetChoice()
        {
            string Choice = Console.ReadLine();
            return Choice;
        }

        static void GetCellReference(ref int Row, ref int Column)
        {
            Console.WriteLine();
            Console.Write("Enter row number: ");
            Row = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter column number: ");
            Column = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
        }


        class Simulation
        {
            protected List<Cell> Grid = new List<Cell>();
            protected List<Ant> Ants = new List<Ant>();
            protected List<Pheromone> Pheromones = new List<Pheromone>();
            protected List<Nest> Nests = new List<Nest>();
            //CHANGE
            protected int NumberOfRows, NumberOfColumns, StartingFoodInNest, StartingNumberOfFoodCells, StartingNumberOfNests, StartingAmountOfFoodInFoodCells;
            //END CHANGE
            protected int StartingAntsInNest, NewPheromoneStrength, PheromoneDecay;

            public Simulation(List<int> SimulationParameters)
            {
                StartingNumberOfNests = SimulationParameters[0];
                NumberOfRows = SimulationParameters[1];
                NumberOfColumns = SimulationParameters[2];
                StartingFoodInNest = SimulationParameters[3];
                StartingNumberOfFoodCells = SimulationParameters[4];
                StartingAntsInNest = SimulationParameters[5];
                NewPheromoneStrength = SimulationParameters[6];
                PheromoneDecay = SimulationParameters[7];
                //CHANGE
                StartingAmountOfFoodInFoodCells = SimulationParameters[8];
                //END CHANGE
                int Row, Column;
                for (Row = 1; Row <= NumberOfRows; Row++)
                {
                    for (Column = 1; Column <= NumberOfColumns; Column++)
                    {
                        Grid.Add(new Cell(Row, Column));
                    }
                }
                SetUpANestAt(2, 4);
                for (int Count = 2; Count <= StartingNumberOfNests; Count++)
                {
                    bool Allowed;
                    do
                    {
                        Allowed = true;
                        Row = RGen.Next(1, NumberOfRows + 1);
                        Column = RGen.Next(1, NumberOfColumns + 1);
                        foreach (Nest N in Nests)
                        {
                            if (N.GetRow() == Row && N.GetColumn() == Column)
                            {
                                Allowed = false;
                            }
                        }
                    } while (!Allowed);
                    SetUpANestAt(Row, Column);
                }
                for (int Count = 1; Count <= StartingNumberOfFoodCells; Count++)
                {
                    bool Allowed;
                    do
                    {
                        Allowed = true;
                        Row = RGen.Next(1, NumberOfRows + 1);
                        Column = RGen.Next(1, NumberOfColumns + 1);
                        foreach (Nest N in Nests)
                        {
                            if (N.GetRow() == Row && N.GetColumn() == Column)
                            {
                                Allowed = false;
                            }
                        }
                    } while (!Allowed);
                    //CHANGE
                    AddFoodToCell(Row, Column, StartingAmountOfFoodInFoodCells);
                    //END CHANGE
                }
            }

            public void SetUpANestAt(int Row, int Column)
            {
                Nests.Add(new Nest(Row, Column, StartingFoodInNest));
                Ants.Add(new QueenAnt(Row, Column, Row, Column));
                for (int Worker = 2; Worker <= StartingAntsInNest; Worker++)
                {
                    Ants.Add(new WorkerAnt(Row, Column, Row, Column));
                }
            }

            public void AddFoodToCell(int Row, int Column, int Quantity)
            {
                Grid[GetIndex(Row, Column)].UpdateFoodInCell(Quantity);
            }

            private int GetIndex(int Row, int Column)
            {
                return (Row - 1) * NumberOfColumns + Column - 1;
            }

            private List<int> GetIndicesOfNeighbours(int Row, int Column)
            {
                List<int> ListOfNeighbours = new List<int>();
                foreach (int RowDirection in new int[] { -1, 0, 1 })
                {
                    foreach (int ColumnDirection in new int[] { -1, 0, 1 })
                    {
                        int NeighbourRow = Row + RowDirection, NeighbourColumn = Column + ColumnDirection;
                        if ((RowDirection != 0 || ColumnDirection != 0) && NeighbourRow >= 1 && NeighbourRow <= NumberOfRows &&
                            NeighbourColumn >= 1 && NeighbourColumn <= NumberOfColumns)
                        {
                            ListOfNeighbours.Add(GetIndex(NeighbourRow, NeighbourColumn));
                        }
                        else
                        {
                            ListOfNeighbours.Add(-1);
                        }
                    }
                }
                return ListOfNeighbours;
            }

            private int GetIndexOfNeighbourWithStrongestPheromone(int Row, int Column)
            {
                int StrongestPheromone = 0, IndexOfStrongestPheromone = -1;
                foreach (int Index in GetIndicesOfNeighbours(Row, Column))
                {
                    if (Index != -1 && GetStrongestPheromoneInCell(Grid[Index]) > StrongestPheromone)
                    {
                        IndexOfStrongestPheromone = Index;
                        StrongestPheromone = GetStrongestPheromoneInCell(Grid[Index]);
                    }
                }
                return IndexOfStrongestPheromone;
            }

            public Nest GetNestInCell(Cell C)
            {
                foreach (Nest N in Nests)
                {
                    if (N.InSameLocation(C))
                    {
                        return N;
                    }
                }
                return null;
            }

            public void UpdateAntsPheromoneInCell(Ant A)
            {
                foreach (Pheromone P in Pheromones)
                {
                    if (P.InSameLocation(A) && P.GetBelongsTo() == A.GetID())
                    {
                        P.UpdateStrength(NewPheromoneStrength);
                        return;
                    }
                }
                Pheromones.Add(new Pheromone(A.GetRow(), A.GetColumn(), A.GetID(), NewPheromoneStrength, PheromoneDecay));
            }

            public int GetNumberOfAntsInCell(Cell C)
            {
                int Count = 0;
                foreach (Ant A in Ants)
                {
                    if (A.InSameLocation(C))
                    {
                        Count++;
                    }
                }
                return Count;
            }

            public int GetNumberOfPheromonesInCell(Cell C)
            {
                int Count = 0;
                foreach (Pheromone P in Pheromones)
                {
                    if (P.InSameLocation(C))
                    {
                        Count++;
                    }
                }
                return Count;
            }

            public int GetStrongestPheromoneInCell(Cell C)
            {
                int Strongest = 0;
                foreach (Pheromone P in Pheromones)
                {
                    if (P.InSameLocation(C))
                    {
                        if (P.GetStrength() > Strongest)
                        {
                            Strongest = P.GetStrength();
                        }
                    }
                }
                return Strongest;
            }

            public string GetDetails()
            {
                string Details = "";
                for (int Row = 1; Row <= NumberOfRows; Row++)
                {
                    for (int Column = 1; Column <= NumberOfColumns; Column++)
                    {
                        Details += $"{Row}, {Column}: ";
                        Cell TempCell = Grid[GetIndex(Row, Column)];
                        if (GetNestInCell(TempCell) != null)
                        {
                            Details += "| Nest |  ";
                        }
                        int NumberOfAnts = GetNumberOfAntsInCell(TempCell);
                        if (NumberOfAnts > 0)
                        {
                            Details += $"| Ants: {NumberOfAnts} |  ";
                        }
                        int NumberOfPheromones = GetNumberOfPheromonesInCell(TempCell);
                        if (NumberOfPheromones > 0)
                        {
                            Details += $"| Pheromones: {NumberOfPheromones} |  ";
                        }
                        int AmountOfFood = TempCell.GetAmountOfFood();
                        if (AmountOfFood > 0)
                        {
                            Details += $"| {AmountOfFood} food |  ";
                        }
                        Details += Environment.NewLine;
                    }
                }
                return Details;
            }

            public string GetAreaDetails(int StartRow, int StartColumn, int EndRow, int EndColumn)
            {
                string Details = "";
                for (int Row = StartRow; Row <= EndRow; Row++)
                {
                    for (int Column = StartColumn; Column <= EndColumn; Column++)
                    {
                        Details += $"{Row}, {Column}: ";
                        Cell TempCell = Grid[GetIndex(Row, Column)];
                        if (GetNestInCell(TempCell) != null)
                        {
                            Details += "| Nest |  ";
                        }
                        int NumberOfAnts = GetNumberOfAntsInCell(TempCell);
                        if (NumberOfAnts > 0)
                        {
                            Details += $"| Ants: {NumberOfAnts} |  ";
                        }
                        int NumberOfPheromones = GetNumberOfPheromonesInCell(TempCell);
                        if (NumberOfPheromones > 0)
                        {
                            Details += $"| Pheromones: {NumberOfPheromones} |  ";
                        }
                        int AmountOfFood = TempCell.GetAmountOfFood();
                        if (AmountOfFood > 0)
                        {
                            Details += $"| {AmountOfFood} food |  ";
                        }
                        Details += Environment.NewLine;
                    }
                }
                return Details;
            }

            public void AddFoodToNest(int Food, int Row, int Column)
            {
                foreach (Nest N in Nests)
                {
                    if (N.GetRow() == Row && N.GetColumn() == Column)
                    {
                        N.ChangeFood(Food);
                        break;
                    }
                }
            }

            public string GetCellDetails(int Row, int Column)
            {
                Cell CurrentCell = Grid[GetIndex(Row, Column)];
                string Details = CurrentCell.GetDetails();
                Nest N = GetNestInCell(CurrentCell);
                if (N != null)
                {
                    Details += $"Nest present ({N.GetFoodLevel()} food){Environment.NewLine}{Environment.NewLine}";
                }
                if (GetNumberOfAntsInCell(CurrentCell) > 0)
                {
                    Details += $"ANTS{Environment.NewLine}";
                    foreach (Ant A in Ants)
                    {
                        if (A.InSameLocation(CurrentCell))
                        {
                            Details += $"{A.GetDetails()}{Environment.NewLine}";
                        }
                    }
                    Details += Environment.NewLine + Environment.NewLine;
                }
                if (GetNumberOfPheromonesInCell(CurrentCell) > 0)
                {
                    Details += $"PHEROMONES{Environment.NewLine}";
                    foreach (Pheromone P in Pheromones)
                    {
                        if (P.InSameLocation(CurrentCell))
                        {
                            Details += $"Ant {P.GetBelongsTo()} with strength of {P.GetStrength()}{Environment.NewLine}{Environment.NewLine}";
                        }
                    }
                    Details += Environment.NewLine + Environment.NewLine;
                }
                return Details;
            }

            public void AdvanceStage(int NumberOfStages)
            {
                for (int Count = 1; Count <= NumberOfStages; Count++)
                {
                    List<Pheromone> PheromonesToDelete = new List<Pheromone>();
                    foreach (Pheromone P in Pheromones)
                    {
                        P.AdvanceStage(Nests, Ants, Pheromones);
                        if (P.GetStrength() == 0)
                        {
                            PheromonesToDelete.Add(P);
                        }
                    }
                    foreach (Pheromone P in PheromonesToDelete)
                    {
                        Pheromones.Remove(P);
                    }
                    foreach (Ant A in Ants)
                    {
                        A.AdvanceStage(Nests, Ants, Pheromones);
                        Cell CurrentCell = Grid[GetIndex(A.GetRow(), A.GetColumn())];
                        if (A.GetFoodCarried() > 0 && A.IsAtOwnNest())
                        {
                            AddFoodToNest(A.GetFoodCarried(), A.GetRow(), A.GetColumn());
                            A.UpdateFoodCarried(-A.GetFoodCarried());
                        }
                        else if (CurrentCell.GetAmountOfFood() > 0 && A.GetFoodCarried() == 0 && A.GetFoodCapacity() > 0)
                        {
                            int FoodObtained;
                            do
                            {
                                FoodObtained = RGen.Next(1, A.GetFoodCapacity() + 1);
                            } while (FoodObtained > CurrentCell.GetAmountOfFood() || (A.GetFoodCarried() + FoodObtained) > A.GetFoodCapacity());
                            CurrentCell.UpdateFoodInCell(-FoodObtained);
                            A.UpdateFoodCarried(FoodObtained);
                        }
                        else
                        {
                            if (A.GetFoodCarried() > 0)
                            {
                                UpdateAntsPheromoneInCell(A);
                            }
                            A.ChooseCellToMoveTo(GetIndicesOfNeighbours(A.GetRow(), A.GetColumn()),
                                                 GetIndexOfNeighbourWithStrongestPheromone(A.GetRow(), A.GetColumn()));
                        }
                    }
                    foreach (Nest N in Nests)
                    {
                        N.AdvanceStage(Nests, Ants, Pheromones);
                    }
                }
            }
        }

        class Entity
        {
            protected int Row, Column, ID;

            public Entity(int StartRow, int StartColumn)
            {
                Row = StartRow;
                Column = StartColumn;
            }

            public bool InSameLocation(Entity E)
            {
                return E.GetRow() == Row && E.GetColumn() == Column;
            }

            public int GetRow()
            {
                return Row;
            }

            public int GetColumn()
            {
                return Column;
            }

            public int GetID()
            {
                return ID;
            }

            public virtual void AdvanceStage(List<Nest> Nests, List<Ant> Ants, List<Pheromone> Pheromones)
            {
            }

            public virtual string GetDetails()
            {
                return "";
            }
        }

        class Cell : Entity
        {
            protected int AmountOfFood;

            public Cell(int StartRow, int StartColumn) : base(StartRow, StartColumn)
            {
                AmountOfFood = 0;
            }

            public int GetAmountOfFood()
            {
                return AmountOfFood;
            }

            public override string GetDetails()
            {
                return $"{base.GetDetails()}{AmountOfFood} food present{Environment.NewLine}{Environment.NewLine}";
            }

            public void UpdateFoodInCell(int Change)
            {
                AmountOfFood += Change;
            }
        }

        class Ant : Entity
        {
            protected int NestRow, NestColumn, AmountOfFoodCarried, Stages, FoodCapacity;
            protected string TypeOfAnt;
            protected static int NextAntID = 1;

            public Ant(int StartRow, int StartColumn, int NestInRow, int NestInColumn)
                : base(StartRow, StartColumn)
            {
                NestRow = NestInRow;
                NestColumn = NestInColumn;
                ID = NextAntID;
                NextAntID++;
                Stages = 0;
                AmountOfFoodCarried = 0;
                FoodCapacity = 0;
                TypeOfAnt = "";
            }

            public virtual int GetFoodCapacity()
            {
                return FoodCapacity;
            }

            public virtual bool IsAtOwnNest()
            {
                return Row == NestRow && Column == NestColumn;
            }

            public override void AdvanceStage(List<Nest> Nests, List<Ant> Ants, List<Pheromone> Pheromones)
            {
                Stages++;
            }

            public override string GetDetails()
            {
                return $"{base.GetDetails()}  Ant {ID}, {TypeOfAnt}, stages alive: {Stages}";
            }

            public virtual void UpdateFoodCarried(int Change)
            {
                AmountOfFoodCarried += Change;
            }

            protected void ChangeCell(int NewCellIndicator, ref int RowToChange, ref int ColumnToChange)
            {
                if (NewCellIndicator > 5)
                {
                    RowToChange++;
                }
                else if (NewCellIndicator < 3)
                {
                    RowToChange--;
                }
                if (new int[] { 0, 3, 6 }.Contains(NewCellIndicator))
                {
                    ColumnToChange--;
                }
                else if (new int[] { 2, 5, 8 }.Contains(NewCellIndicator))
                {
                    ColumnToChange++;
                }
            }

            protected int ChooseRandomNeighbour(List<int> ListOfNeighbours)
            {
                int RNo = 0;
                do
                {
                    RNo = RGen.Next(0, ListOfNeighbours.Count);
                } while (ListOfNeighbours[RNo] == -1);
                return RNo;
            }

            public virtual void ChooseCellToMoveTo(List<int> ListOfNeighbours, int IndexOfNeighbourWithStrongestPheromone)
            {
            }

            public virtual int GetFoodCarried()
            {
                return AmountOfFoodCarried;
            }

            public virtual int GetNestRow()
            {
                return NestRow;
            }

            public virtual int GetNestColumn()
            {
                return NestColumn;
            }

            public virtual string GetTypeOfAnt()
            {
                return TypeOfAnt;
            }
        }

        class QueenAnt : Ant
        {
            public QueenAnt(int StartRow, int StartColumn, int NestInRow, int NestInColumn)
                : base(StartRow, StartColumn, NestInRow, NestInColumn)
            {
                TypeOfAnt = "queen";
            }
        }

        class WorkerAnt : Ant
        {
            public WorkerAnt(int StartRow, int StartColumn, int NestInRow, int NestInColumn)
                : base(StartRow, StartColumn, NestInRow, NestInColumn)
            {
                TypeOfAnt = "worker";
                FoodCapacity = 30;
            }

            public override string GetDetails()
            {
                return $"{base.GetDetails()}, carrying {AmountOfFoodCarried} food, home nest is at {NestRow} {NestColumn}";
            }

            public override void ChooseCellToMoveTo(List<int> ListOfNeighbours, int IndexOfNeighbourWithStrongestPheromone)
            {
                if (AmountOfFoodCarried > 0)
                {
                    if (Row > NestRow)
                    {
                        Row--;
                    }
                    else if (Row < NestRow)
                    {
                        Row++;
                    }
                    if (Column > NestColumn)
                    {
                        Column--;
                    }
                    else if (Column < NestColumn)
                    {
                        Column++;
                    }
                }
                else if (IndexOfNeighbourWithStrongestPheromone == -1)
                {
                    int IndexToUse = ChooseRandomNeighbour(ListOfNeighbours);
                    ChangeCell(IndexToUse, ref Row, ref Column);
                }
                else
                {
                    int IndexToUse = ListOfNeighbours.IndexOf(IndexOfNeighbourWithStrongestPheromone);
                    ChangeCell(IndexToUse, ref Row, ref Column);
                }
            }
        }

        class Nest : Entity
        {
            protected int FoodLevel, NumberOfQueens;
            protected static int NextNestID = 1;

            public Nest(int StartRow, int StartColumn, int StartFood) : base(StartRow, StartColumn)
            {
                FoodLevel = StartFood;
                NumberOfQueens = 1;
                ID = NextNestID;
                NextNestID++;
            }

            public void ChangeFood(int Change)
            {
                FoodLevel += Change;
                if (FoodLevel < 0)
                {
                    FoodLevel = 0;
                }
            }

            public int GetFoodLevel()
            {
                return FoodLevel;
            }

            public override void AdvanceStage(List<Nest> Nests, List<Ant> Ants, List<Pheromone> Pheromones)
            {
                if (Ants == null)
                {
                    return;
                }
                int AntsToCull = 0;
                int Count = 0;
                int AntsInNestCount = 0;
                foreach (Ant A in Ants)
                {
                    if (A.GetNestRow() == Row && A.GetNestColumn() == Column)
                    {
                        if (A.GetTypeOfAnt() == "queen")
                        {
                            Count += 10;
                        }
                        else
                        {
                            Count += 2;
                            AntsInNestCount++;
                        }
                    }
                }
                ChangeFood(-Count);
                if (FoodLevel == 0 && AntsInNestCount > 0)
                {
                    AntsToCull++;
                }
                if (FoodLevel < AntsInNestCount)
                {
                    AntsToCull++;
                }
                if (FoodLevel < AntsInNestCount * 5)
                {
                    AntsToCull++;
                    if (AntsToCull > AntsInNestCount)
                    {
                        AntsToCull = AntsInNestCount;
                    }
                    for (int A = 1; A <= AntsToCull; A++)
                    {
                        int RPos;
                        do
                        {
                            RPos = RGen.Next(0, Ants.Count);
                        } while (!(Ants[RPos].GetNestRow() == Row && Ants[RPos].GetNestColumn() == Column));
                        if (Ants[RPos].GetTypeOfAnt() == "queen")
                        {
                            NumberOfQueens--;
                        }
                        Ants.RemoveAt(RPos);
                    }
                }
                else
                {
                    for (int A = 1; A <= NumberOfQueens; A++)
                    {
                        int RNo1 = RGen.Next(0, 100);
                        if (RNo1 < 50)
                        {
                            int RNo2 = RGen.Next(0, 100);
                            if (RNo2 < 2)
                            {
                                Ants.Add(new QueenAnt(Row, Column, Row, Column));
                                NumberOfQueens++;
                            }
                            else
                            {
                                Ants.Add(new WorkerAnt(Row, Column, Row, Column));
                            }
                        }
                    }
                }
            }
        }

        class Pheromone : Entity
        {
            protected int Strength, PheromoneDecay, BelongsTo;

            public Pheromone(int Row, int Column, int BelongsToAnt, int InitialStrength, int Decay)
                : base(Row, Column)
            {
                BelongsTo = BelongsToAnt;
                Strength = InitialStrength;
                PheromoneDecay = Decay;
            }

            public override void AdvanceStage(List<Nest> Nests, List<Ant> Ants, List<Pheromone> Pheromones)
            {
                Strength -= PheromoneDecay;
                if (Strength < 0)
                {
                    Strength = 0;
                }
            }

            public void UpdateStrength(int Change)
            {
                Strength += Change;
            }

            public int GetStrength()
            {
                return Strength;
            }

            public int GetBelongsTo()
            {
                return BelongsTo;
            }
        }
    }
}
