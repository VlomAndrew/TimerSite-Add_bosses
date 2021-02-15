using System;

namespace FirstProject.Models
{
    public class HistoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastTime { get; set; }
        public string BossName { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return String.Format("{0} | {1} | {2} | {3}",Name,Type,BossName,LastTime);
        }
    }
}