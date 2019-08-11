using System;

namespace Stateless
{
    partial class StateMachine<TState, TTrigger>
    {
        /// <summary>
        /// Associates configured parameters with an underlying trigger value.
        /// </summary>
        public abstract class TriggerWithParameters
        {
            readonly TriggerWithParametersTypeKey _key;

            /// <summary>
            /// Create a configured trigger.
            /// </summary>
            /// <param name="underlyingTrigger">Trigger represented by this trigger configuration.</param>
            /// <param name="argumentTypes">The argument types expected by the trigger.</param>
            public TriggerWithParameters(TTrigger underlyingTrigger, params Type[] argumentTypes)
            {
                _key = new TriggerWithParametersTypeKey(underlyingTrigger, argumentTypes);
            }

            /// <summary>
            /// Gets the underlying trigger value that has been configured.
            /// </summary>
            public TTrigger Trigger { get { return _key.Trigger; } }

            /// <summary>
            /// Get the underlying key type that represents the trigger.
            /// </summary>
            internal TriggerWithParametersTypeKey InternalKey { get { return _key; } }

            /// <summary>
            /// Ensure that the supplied arguments are compatible with those configured for this
            /// trigger.
            /// </summary>
            /// <param name="args"></param>
            public void ValidateParameters(object[] args)
            {
                if (args == null) throw new ArgumentNullException(nameof(args));

                ParameterConversion.Validate(args, _key._argumentTypes);
            }

            internal struct TriggerWithParametersTypeKey
            {
                public readonly Type[] _argumentTypes;

                public TriggerWithParametersTypeKey(TTrigger underlyingTrigger, Type[] argumentTypes)
                {
                    Trigger = underlyingTrigger;
                    _argumentTypes = argumentTypes ?? throw new ArgumentNullException(nameof(argumentTypes));
                }

                public TTrigger Trigger { get; private set; }

                public override bool Equals(object obj)
                {
                    if (obj is TriggerWithParametersTypeKey otherKey)
                    {
                        return otherKey.Trigger.Equals(this.Trigger) && this._argumentTypes.Equals(otherKey._argumentTypes);
                    }
                    return false;
                }

                public override int GetHashCode()
                {
                    //TODO: getting the hash code of an array is troublesome
                    return Tuple.Create(Trigger, _argumentTypes).GetHashCode();
                }
            }
        }

        /// <summary>
        /// A configured trigger with one required argument.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first argument.</typeparam>
        public class TriggerWithParameters<TArg0> : TriggerWithParameters
        {
            /// <summary>
            /// Create a configured trigger.
            /// </summary>
            /// <param name="underlyingTrigger">Trigger represented by this trigger configuration.</param>
            public TriggerWithParameters(TTrigger underlyingTrigger)
                : base(underlyingTrigger, typeof(TArg0))
            {
            }
        }

        /// <summary>
        /// A configured trigger with two required arguments.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first argument.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument.</typeparam>
        public class TriggerWithParameters<TArg0, TArg1> : TriggerWithParameters
        {
            /// <summary>
            /// Create a configured trigger.
            /// </summary>
            /// <param name="underlyingTrigger">Trigger represented by this trigger configuration.</param>
            public TriggerWithParameters(TTrigger underlyingTrigger)
                : base(underlyingTrigger, typeof(TArg0), typeof(TArg1))
            {
            }
        }

        /// <summary>
        /// A configured trigger with three required arguments.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first argument.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument.</typeparam>
        /// <typeparam name="TArg2">The type of the third argument.</typeparam>
        public class TriggerWithParameters<TArg0, TArg1, TArg2> : TriggerWithParameters
        {
            /// <summary>
            /// Create a configured trigger.
            /// </summary>
            /// <param name="underlyingTrigger">Trigger represented by this trigger configuration.</param>
            public TriggerWithParameters(TTrigger underlyingTrigger)
                : base(underlyingTrigger, typeof(TArg0), typeof(TArg1), typeof(TArg2))
            {
            }
        }
    }
}
