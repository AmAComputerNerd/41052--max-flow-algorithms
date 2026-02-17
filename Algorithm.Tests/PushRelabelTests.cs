using Algorithm.Common;
using PushRelabelAlgorithm;

namespace Algorithm.Tests;

[TestClass]
public sealed class PushRelabelTests : MaxFlowTestCase
{
    protected override IMaxFlowAlgorithm CreateAlgorithm(FlowGraph graph)
    {
        return new PushRelabel(graph);
    }
}
