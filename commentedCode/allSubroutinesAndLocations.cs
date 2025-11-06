namespace AntSimCS
{
    class Program
    {



        static void Main() {}
        static void DisplayMenu() {}
        static string GetChoice() {}
        static void GetCellReference(ref int Row, ref int Column) {}



        class Simulation
        {
            public Simulation(List<int> SimulationParameters) { }
            public void SetUpANestAt(int Row, int Column) { }
            public void AddFoodToCell(int Row, int Column, int Quantity) { }
            private int GetIndex(int Row, int Column) { }
            private List<int> GetIndicesOfNeighbours(int Row, int Column) { }
            private int GetIndexOfNeighbourWithStrongestPheromone(int Row, int Column) { }
            public Nest GetNestInCell(Cell C) { }
            public void UpdateAntsPheromoneInCell(Ant A) { }
            public int GetNumberOfAntsInCell(Cell C) { }
            public int GetNumberOfPheromonesInCell(Cell C) { }
            public int GetStrongestPheromoneInCell(Cell C) { }
            public string GetDetails() { }
            public string GetAreaDetails(int StartRow, int StartColumn, int EndRow, int EndColumn) { }
            public void AddFoodToNest(int Food, int Row, int Column) { }
            public string GetCellDetails(int Row, int Column) { }
            public void AdvanceStage(int NumberOfStages) { }
        }

        class Entity
        {
            public Entity(int StartRow, int StartColumn) { }
            public bool InSameLocation(Entity E) { }
            public int GetRow() { }
            public int GetColumn() { }
            public int GetID() { }
            public virtual void AdvanceStage(List<Nest> Nests, List<Ant> Ants, List<Pheromone> Pheromones) { }
            public virtual string GetDetails() { }
        }

        class Cell : Entity
        {
            public Cell(int StartRow, int StartColumn) : base(StartRow, StartColumn) { }
            public int GetAmountOfFood() { }
            public override string GetDetails() { }
            public void UpdateFoodInCell(int Change) { }
        }

        class Ant : Entity
        {
            public Ant(int StartRow, int StartColumn, int NestInRow, int NestInColumn)
                : base(StartRow, StartColumn) { }
            public virtual int GetFoodCapacity() { }
            public virtual bool IsAtOwnNest() { }
            public override void AdvanceStage(List<Nest> Nests, List<Ant> Ants, List<Pheromone> Pheromones) { }
            public override string GetDetails() { }
            public virtual void UpdateFoodCarried(int Change) { }
            protected void ChangeCell(int NewCellIndicator, ref int RowToChange, ref int ColumnToChange) { }
            protected int ChooseRandomNeighbour(List<int> ListOfNeighbours) { }
            public virtual void ChooseCellToMoveTo(List<int> ListOfNeighbours, int IndexOfNeighbourWithStrongestPheromone) { }
            public virtual int GetFoodCarried() { }
            public virtual int GetNestRow() { }
            public virtual int GetNestColumn() { }
            public virtual string GetTypeOfAnt() { }
        }

        class QueenAnt : Ant
        {
            public QueenAnt(int StartRow, int StartColumn, int NestInRow, int NestInColumn)
                : base(StartRow, StartColumn, NestInRow, NestInColumn) { }
        }

        class WorkerAnt : Ant
        {
            public WorkerAnt(int StartRow, int StartColumn, int NestInRow, int NestInColumn)
                : base(StartRow, StartColumn, NestInRow, NestInColumn) { }
            public override string GetDetails() { }
            public override void ChooseCellToMoveTo(List<int> ListOfNeighbours, int IndexOfNeighbourWithStrongestPheromone) { }
        }

        class Nest : Entity
        {
            public Nest(int StartRow, int StartColumn, int StartFood) : base(StartRow, StartColumn) { }
            public void ChangeFood(int Change) { }
            public int GetFoodLevel() { }
            public override void AdvanceStage(List<Nest> Nests, List<Ant> Ants, List<Pheromone> Pheromones) { }
        }
        
        class Pheromone : Entity
        {
            public Pheromone(int Row, int Column, int BelongsToAnt, int InitialStrength, int Decay)
                : base(Row, Column) { }
            public override void AdvanceStage(List<Nest> Nests, List<Ant> Ants, List<Pheromone> Pheromones) { }
            public void UpdateStrength(int Change) { }
            public int GetStrength() { }
            public int GetBelongsTo() { }
        }
    }
}