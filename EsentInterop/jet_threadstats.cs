﻿//-----------------------------------------------------------------------
// <copyright file="jet_threadstats.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Vista
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Contains cumulative statistics on the work performed by the database
    /// engine on the current thread. This information is returned via
    /// <see cref="VistaApi.JetGetThreadStats"/>.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1300:ElementMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [Serializable]
    public struct JET_THREADSTATS : IEquatable<JET_THREADSTATS>
    {
        /// <summary>
        /// Number of pages visited.
        /// </summary>
        private int pagesReferenced;

        /// <summary>
        /// Number of pages read from disk.
        /// </summary>
        private int pagesRead;

        /// <summary>
        /// Number of pages preread.
        /// </summary>
        private int pagesPreread;

        /// <summary>
        /// Number of pages dirtied.
        /// </summary>
        private int pagesDirtied;

        /// <summary>
        /// Pages redirtied.
        /// </summary>
        private int pagesRedirtied;

        /// <summary>
        /// Number of log records generated.
        /// </summary>
        private int numLogRecords;

        /// <summary>
        /// Number of bytes logged.
        /// </summary>
        private int loggedBytes;

        /// <summary>
        /// Gets the total number of database pages visited by the database
        /// engine on the current thread.
        /// </summary>
        public int cPageReferenced
        {
            get { return this.pagesReferenced; }
            internal set { this.pagesReferenced = value; }
        }

        /// <summary>
        /// Gets the total number of database pages fetched from disk by the
        /// database engine on the current thread.
        /// </summary>
        public int cPageRead
        {
            get { return this.pagesRead; }
            internal set { this.pagesRead = value; }
        }

        /// <summary>
        /// Gets the total number of database pages prefetched from disk by
        /// the database engine on the current thread.
        /// </summary>
        public int cPagePreread
        {
            get { return this.pagesPreread; }
            internal set { this.pagesPreread = value; }
        }

        /// <summary>
        /// Gets the total number of database pages, with no unwritten changes,
        /// that have been modified by the database engine on the current thread.
        /// </summary>
        public int cPageDirtied
        {
            get { return this.pagesDirtied; }
            internal set { this.pagesDirtied = value; }
        }

        /// <summary>
        /// Gets the total number of database pages, with unwritten changes, that
        /// have been modified by the database engine on the current thread.
        /// </summary>
        public int cPageRedirtied
        {
            get { return this.pagesRedirtied; }
            internal set { this.pagesRedirtied = value; }
        }

        /// <summary>
        /// Gets the total number of transaction log records that have been
        /// generated by the database engine on the current thread.
        /// </summary>
        public int cLogRecord
        {
            get { return this.numLogRecords; }
            internal set { this.numLogRecords = value; }
        }

        /// <summary>
        /// Gets the total size, in bytes, of transaction log records that
        /// have been generated by the database engine on the current thread.
        /// </summary>
        public int cbLogRecord
        {
            get { return this.loggedBytes; }
            internal set { this.loggedBytes = value; }
        }

        /// <summary>
        /// Add the stats in two JET_THREADSTATS structures.
        /// </summary>
        /// <param name="t1">The first JET_THREADSTATS.</param>
        /// <param name="t2">The second JET_THREADSTATS.</param>
        /// <returns>A JET_THREADSTATS containing the result of adding the stats in t1 and t2.</returns>
        public static JET_THREADSTATS operator +(JET_THREADSTATS t1, JET_THREADSTATS t2)
        {
            checked
            {
                return new JET_THREADSTATS
                {
                    cPageReferenced = t1.cPageReferenced + t2.cPageReferenced,
                    cPageRead = t1.cPageRead + t2.cPageRead,
                    cPagePreread = t1.cPagePreread + t2.cPagePreread,
                    cPageDirtied = t1.cPageDirtied + t2.cPageDirtied,
                    cPageRedirtied = t1.cPageRedirtied + t2.cPageRedirtied,
                    cLogRecord = t1.cLogRecord + t2.cLogRecord,
                    cbLogRecord = t1.cbLogRecord + t2.cbLogRecord,
                };                
            }
        }

        /// <summary>
        /// Calculate the difference in stats between two JET_THREADSTATS structures.
        /// </summary>
        /// <param name="t1">The first JET_THREADSTATS.</param>
        /// <param name="t2">The second JET_THREADSTATS.</param>
        /// <returns>A JET_THREADSTATS containing the difference in stats between t1 and t2.</returns>
        public static JET_THREADSTATS operator -(JET_THREADSTATS t1, JET_THREADSTATS t2)
        {
            checked
            {
                return new JET_THREADSTATS
                {
                    cPageReferenced = t1.cPageReferenced - t2.cPageReferenced,
                    cPageRead = t1.cPageRead - t2.cPageRead,
                    cPagePreread = t1.cPagePreread - t2.cPagePreread,
                    cPageDirtied = t1.cPageDirtied - t2.cPageDirtied,
                    cPageRedirtied = t1.cPageRedirtied - t2.cPageRedirtied,
                    cLogRecord = t1.cLogRecord - t2.cLogRecord,
                    cbLogRecord = t1.cbLogRecord - t2.cbLogRecord,
                };
            }
        }

        /// <summary>
        /// Determines whether two specified instances of JET_THREADSTATS
        /// are equal.
        /// </summary>
        /// <param name="lhs">The first instance to compare.</param>
        /// <param name="rhs">The second instance to compare.</param>
        /// <returns>True if the two instances are equal.</returns>
        public static bool operator ==(JET_THREADSTATS lhs, JET_THREADSTATS rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether two specified instances of JET_THREADSTATS
        /// are not equal.
        /// </summary>
        /// <param name="lhs">The first instance to compare.</param>
        /// <param name="rhs">The second instance to compare.</param>
        /// <returns>True if the two instances are not equal.</returns>
        public static bool operator !=(JET_THREADSTATS lhs, JET_THREADSTATS rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Gets a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:N0} page reference{1}, ", this.cPageReferenced, GetPluralS(this.cPageReferenced));
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:N0} page{1} read, ", this.cPageRead, GetPluralS(this.cPageRead));
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:N0} page{1} preread, ", this.cPagePreread, GetPluralS(this.cPagePreread));
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:N0} page{1} dirtied, ", this.cPageDirtied, GetPluralS(this.cPageDirtied));
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:N0} page{1} redirtied, ", this.cPageRedirtied, GetPluralS(this.cPageRedirtied));
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:N0} log record{1}, ", this.cLogRecord, GetPluralS(this.cLogRecord));
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:N0} byte{1} logged", this.cbLogRecord, GetPluralS(this.cbLogRecord));
            return sb.ToString();
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>True if the two instances are equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((JET_THREADSTATS)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.cPageReferenced
                   ^ this.cPageRead << 1
                   ^ this.cPagePreread << 2
                   ^ this.cPageDirtied << 3
                   ^ this.cPageRedirtied << 4
                   ^ this.cLogRecord << 5
                   ^ this.cbLogRecord << 6;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="other">An instance to compare with this instance.</param>
        /// <returns>True if the two instances are equal.</returns>
        public bool Equals(JET_THREADSTATS other)
        {
            return this.cbLogRecord == other.cbLogRecord
                   && this.cLogRecord == other.cLogRecord
                   && this.cPageDirtied == other.cPageDirtied
                   && this.cPagePreread == other.cPagePreread
                   && this.cPageRead == other.cPageRead
                   && this.cPageRedirtied == other.cPageRedirtied
                   && this.cPageReferenced == other.cPageReferenced;
        }

        /// <summary>
        /// Sets the fields of the object from a NATIVE_THREADSTATS struct.
        /// </summary>
        /// <param name="value">
        /// The native threadstats to set the values from.
        /// </param>
        internal void SetFromNativeThreadstats(NATIVE_THREADSTATS value)
        {
            this.cPageReferenced = checked((int)value.cPageReferenced);
            this.cPageRead = checked((int)value.cPageRead);
            this.cPagePreread = checked((int)value.cPagePreread);
            this.cPageDirtied = checked((int)value.cPageDirtied);
            this.cPageRedirtied = checked((int)value.cPageRedirtied);
            this.cLogRecord = checked((int)value.cLogRecord);
            this.cbLogRecord = checked((int)value.cbLogRecord);
        }

        /// <summary>
        /// Get the plural suffix ('s') for the given number.
        /// </summary>
        /// <param name="n">The number.</param>
        /// <returns>The letter 's' if n is greater than 1.</returns>
        private static string GetPluralS(int n)
        {
            return n == 1 ? String.Empty : "s";
        }
    }

    /// <summary>
    /// The native version of the JET_THREADSTATS structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    internal struct NATIVE_THREADSTATS
    {
        /// <summary>
        /// The size of a NATIVE_THREADSTATS structure.
        /// </summary>
        public static readonly int Size = Marshal.SizeOf(typeof(NATIVE_THREADSTATS));

        /// <summary>
        /// The size of the structure.
        /// </summary>
        public uint cbStruct;

        /// <summary>
        /// The total number of database pages visited by the database
        /// engine on the current thread.
        /// </summary>
        public uint cPageReferenced;

        /// <summary>
        /// The total number of database pages fetched from disk by the
        /// database engine on the current thread.
        /// </summary>
        public uint cPageRead;

        /// <summary>
        /// The total number of database pages prefetched from disk by
        /// the database engine on the current thread.
        /// </summary>
        public uint cPagePreread;

        /// <summary>
        /// The total number of database pages, with no unwritten changes,
        /// that have been modified by the database engine on the current thread.
        /// </summary>
        public uint cPageDirtied;

        /// <summary>
        /// The total number of database pages, with unwritten changes, that
        /// have been modified by the database engine on the current thread.
        /// </summary>
        public uint cPageRedirtied;

        /// <summary>
        /// The total number of transaction log records that have been
        /// generated by the database engine on the current thread.
        /// </summary>
        public uint cLogRecord;

        /// <summary>
        /// The total size, in bytes, of transaction log records that
        /// have been generated by the database engine on the current thread.
        /// </summary>
        public uint cbLogRecord;
    }
}