namespace Conflux.NodeCanvas.Serialization
{
    internal interface IDefaultCheckable
    {
        /// <summary>
        /// Compare this object against the compile-time constant value to
        /// determine if it matches. If it does, it is assumed that this
        /// object can be recreated from that compile-time constant.
        /// </summary>
        /// <param name="value">The compile-time constant.</param>
        /// <returns>Whether the object matches the compile-time constant.</returns>
        bool IsDefaultValue(object? value);
    }
}
