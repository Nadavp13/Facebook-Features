using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    internal class UniqueExceptions
    {
        public class FriendNotFoundException : Exception
        {
            public FriendNotFoundException()
            {
            }

            public FriendNotFoundException(string message)
                : base(message)
            {
            }

            public FriendNotFoundException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class NoAccessToCollectionException : Exception
        {
            public NoAccessToCollectionException()
            {
            }

            public NoAccessToCollectionException(string message)
                : base(message)
            {
            }

            public NoAccessToCollectionException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class NoCollectionFoundException : Exception
        {
            public NoCollectionFoundException()
            {
            }

            public NoCollectionFoundException(string message)
                : base(message)
            {
            }

            public NoCollectionFoundException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class NotExactlyOneException : Exception
        {
            public NotExactlyOneException()
            {
            }

            public NotExactlyOneException(string message)
                : base(message)
            {
            }

            public NotExactlyOneException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class NoMoreImagesException : Exception
        {
            public NoMoreImagesException()
            {
            }

            public NoMoreImagesException(string message)
                : base(message)
            {
            }

            public NoMoreImagesException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class NoPostFoundException : Exception
        {
            public NoPostFoundException()
            {
            }

            public NoPostFoundException(string message)
                : base(message)
            {
            }

            public NoPostFoundException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class NoPostsAfterSearchingException : Exception
        {
            public NoPostsAfterSearchingException()
            {
            }

            public NoPostsAfterSearchingException(string message)
                : base(message)
            {
            }

            public NoPostsAfterSearchingException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class QuantityNotInFormatException : Exception
        {
            public QuantityNotInFormatException()
            {
            }

            public QuantityNotInFormatException(string message)
                : base(message)
            {
            }

            public QuantityNotInFormatException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}