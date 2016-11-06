//-----------------------------------------------------------------------
// <copyright file="HumanEventArgs.cs" company="My Company">
//     Some info
// </copyright>
//-----------------------------------------------------------------------

namespace WPF_People_lab
{
    using System;

    /// <summary>
    /// Class for events arguments
    /// </summary>
    public class HumanEventArgs : EventArgs
    {
        /// <summary>
        /// Message for sender
        /// </summary>
        public readonly string Msg;

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanEventArgs"/> class.
        /// </summary>
        /// <param name="message">Message for sender</param>
        public HumanEventArgs(string message)
        {
            this.Msg = message;
        } 
    }
}
