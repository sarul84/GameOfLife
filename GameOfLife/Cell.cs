namespace GameOfLife
{
    public class Cell
    {
        public bool IsAlive { get; set; }

        public Cell(bool isAlive)
        {
            this.IsAlive = isAlive;
        }

        public override string ToString()
        {
            return (IsAlive ? " 1 " : " 0 ");
        }
    }
}
