using Algorithm.Common;
using EdmondsKarpAlgorithm;

namespace Algorithm.Tests
{
    [TestClass]
    public sealed class EdmondsKarpTests : MaxFlowTestCase
    {
        protected override IMaxFlowAlgorithm CreateAlgorithm(FlowGraph graph)
        {
            return new EdmondsKarp(graph);
        }
    }
}
