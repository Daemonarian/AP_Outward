using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Conflux.Context
{
    /// <summary>
    /// Provides an interface for properly constructing a NodeCanvas graph of
    /// any particular type.
    /// </summary>
    internal interface INodeCanvasGraphContext
    {
        /// <summary>
        /// The Conflux script to compile.
        /// </summary>
        /// <returns>The Conflux script.</returns>
        abstract ConfluxScript Script { get; }

        /// <summary>
        /// Get the graph type produced by this context.
        /// </summary>
        /// <returns>The graph type as a string.</returns>
        abstract string GraphType { get; }

        /// <summary>
        /// Get the base type of nodes for this graph.
        /// </summary>
        /// <returns>The allowed base type for nodes.</returns>
        abstract Type NodeBaseType { get; }

        /// <summary>
        /// Get the base type of connections for this graph.
        /// </summary>
        /// <returns>The allowed base type for connections.</returns>
        abstract Type ConnectionBaseType { get; }

        /// <summary>
        /// Build a blackboard parameter that references the specified blackboard variable.
        /// </summary>
        /// <typeparam name="T">The type of the blackboard parameter.</typeparam>
        /// <param name="name">The name of the blackboard variable to reference.</param>
        /// <returns>The blackboard parameter.</returns>
        abstract BBParameter<T> BuildBBParameter<T>(string name);

        /// <summary>
        /// Build an appropriate connection for the context.
        /// </summary>
        /// <param name="source">The source node.</param>
        /// <param name="target">The target node.</param>
        /// <returns>The new connection.</returns>
        abstract Connection BuildConnection(Node source, Node target);

        /// <summary>
        /// Build a node which successfully finishes the Graph execution when
        /// given no out connections.
        /// </summary>
        /// <returns>The finish node.</returns>
        abstract Node BuildTerminalNode();

        /// <summary>
        /// Build a node which selects between its two outputs.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The if node.</returns>
        abstract Node BuildIfNode(ConditionTask condition);

        /// <summary>
        /// Build a node which executes an action.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>The do node.</returns>
        abstract Node BuildDoNode(ActionTask action);
    }
}
