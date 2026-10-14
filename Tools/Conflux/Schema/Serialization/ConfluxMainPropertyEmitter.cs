using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Conflux.Schema.Serialization
{
    /// <summary>
    /// An YAML emitter implementation that in-lines Conflux main-properties.
    /// </summary>
    internal class ConfluxMainPropertyEmitter : IEmitter
    {
        public const string TagPrefix = "!ConfluxMainProperty:";

        public IEmitter NextEmitter { get; init; }

        private NestedEmitter CurrentEmitter { get; set; }

        public ConfluxMainPropertyEmitter(IEmitter nextEmitter)
        {
            NextEmitter = nextEmitter;
            CurrentEmitter = new PassThruNestedEmitter(this);
        }

        public void Emit(ParsingEvent @event)
        {
            if (@event is MappingStart ms &&
                !ms.Tag.IsEmpty &&
                ms.Tag.Value.StartsWith(TagPrefix))
            {
                CurrentEmitter = new InliningNestedEmitter(this, CurrentEmitter);
            }

            var done = CurrentEmitter.Emit(@event);
            if (done)
            {
                CurrentEmitter = CurrentEmitter.Next;
            }
        }

        /// <summary>
        /// Represents one of a stack of nested emitters, each handling the in-lining of a single object.
        /// </summary>
        /// <param name="parent">The main emitter.</param>
        /// <param name="next">The next emitter in the stack.</param>
        private abstract class NestedEmitter(ConfluxMainPropertyEmitter parent, NestedEmitter? next = null)
        {
            /// <summary>
            /// The main emitter.
            /// </summary>
            public ConfluxMainPropertyEmitter Parent { get; init; } = parent;

            private readonly NestedEmitter? _next = next;

            /// <summary>
            /// The next nested emitter in the stack.
            /// </summary>
            public NestedEmitter Next => _next ?? this;

            /// <summary>
            /// Process a single parsing event.
            /// </summary>
            /// <param name="event">The YAML parsing event.</param>
            /// <returns>Whether this nested emitter is done after processing this event.</returns>
            public abstract bool Emit(ParsingEvent @event);
        }

        private class PassThruNestedEmitter(ConfluxMainPropertyEmitter parent) : NestedEmitter(parent)
        {
            public override bool Emit(ParsingEvent @event)
            {
                Parent.NextEmitter.Emit(@event);
                return false;
            }
        }

        private class InliningNestedEmitter(ConfluxMainPropertyEmitter parent, NestedEmitter next) : NestedEmitter(parent, next)
        {
            private List<ParsingEvent> _buffer = [];

            private int _depth = 0;

            public override bool Emit(ParsingEvent @event)
            {
                _buffer.Add(@event);

                if (@event is MappingStart)
                {
                    _depth++;
                }
                else if (@event is MappingEnd)
                {
                    _depth--;
                }

                if (_depth > 0)
                {
                    return false;
                }

                InlineBuffer();
                FlushBuffer();
                return true;
            }

            private void InlineBuffer()
            {
                // check that with start with a tagged mapping start
                // and end with a mapping end
                if (_buffer.Count > 0 &&
                    _buffer[0] is MappingStart ms &&
                    !ms.Tag.IsEmpty &&
                    ms.Tag.Value.StartsWith(TagPrefix))
                {
                    var tagPayload = ms.Tag.Value[TagPrefix.Length..];
                    var parts = tagPayload.Split(':');
                    var expectedKey = parts[0];
                    var isForced = parts.Length > 1 && bool.Parse(parts[1]);

                    // check that the next event is the main-property key
                    if (_buffer.Count > 1 &&
                        _buffer[1] is Scalar keyScalar &&
                        keyScalar.Value == expectedKey)
                    {
                        // check that the main property is forced or not a mapping
                        if (isForced ||
                            (_buffer.Count > 2 &&
                            _buffer[2] is not MappingStart))
                        {
                            // find the index of the event that follows the value of
                            // the main-property
                            var nextIndex = 2;
                            var depth = 0;
                            while (nextIndex < _buffer.Count)
                            {
                                var currEvent = _buffer[nextIndex];
                                if (currEvent is MappingStart || currEvent is SequenceStart)
                                {
                                    depth++;
                                }
                                else if (currEvent is MappingEnd || currEvent is SequenceEnd)
                                {
                                    depth--;
                                }

                                nextIndex++;

                                if (depth <= 0)
                                {
                                    break;
                                }
                            }

                            // check that this index is the end of the main object
                            if (nextIndex == _buffer.Count - 1 &&
                                _buffer[nextIndex] is MappingEnd)
                            {
                                // strip the first 2 events and the last event from the buffer
                                _buffer = _buffer[2..^1];
                                return;
                            }
                        }
                    }

                    // strip the tag from the mapping start
                    _buffer[0] = new MappingStart(ms.Anchor, null, ms.IsImplicit, ms.Style);
                }
            }

            private void FlushBuffer()
            {
                foreach (var @event in _buffer)
                {
                    Next.Emit(@event);
                }
            }
        }
    }
}
