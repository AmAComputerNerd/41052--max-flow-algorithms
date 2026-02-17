using Algorithm.Common;
using DinicsAlgorithm;

namespace Algorithm.Tests;

[TestClass]
public sealed class DinicsAlgorithmTests : MaxFlowTestCase
{
    protected override IMaxFlowAlgorithm CreateAlgorithm(FlowGraph graph)
    {
        return new Dinics(graph);
    }
}