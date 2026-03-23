using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class NewTestData
{
    public int[] QuestionIds { get; set; }
    public int Amount { get; set; }
    public int TechnologyId { get; set; }
}
