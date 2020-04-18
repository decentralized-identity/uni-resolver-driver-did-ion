using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Tests the <see cref="Identifier.IsSupported(string)" /> method
    /// of the <see cref="Identifier" /> class.
    /// </summary>
    [TestClass]
    public class Identifier_IsSupportedShould
    {
        /// <summary>
        /// Tests that false is returned by the method
        /// when a non supported identifier is passed.
        /// </summary>
        [TestMethod]
        public void IsSupported_InvalidInput_ReturnFalse()
        {
            Assert.IsFalse(Identifier.IsSupported(string.Empty));
            Assert.IsFalse(Identifier.IsSupported(null));
            Assert.IsFalse(Identifier.IsSupported(""));
        }

        /// <summary>
        /// Tests that false is returned by the method
        /// when a non supported identifier is passed.
        /// </summary>
        [TestMethod]
        public void IsSupported_InputNotSupported_ReturnFalse()
        {
            const string NotSupported = "did:sov:WRfXPg8dantKVubE3HX8pw";
            Assert.IsFalse(Identifier.IsSupported(NotSupported));
        }

        /// <summary>
        /// Tests that true is returned by the method
        /// when an ION identifier is passed.
        /// </summary>
        [TestMethod]
        public void IsSupported_InputSupported_ReturnTrue()
        {
            const string Supported = "did:ion:WRfXPg8dantKVubE3HX8pw";
            Assert.IsTrue(Identifier.IsSupported(Supported));
        }

        /// <summary>
        /// Tests that true is returned by the method
        /// when a self-resolving ION identifier is passed.
        /// </summary>
        [TestMethod]
        public void IsSupported_SelfResolvingInputSupported_ReturnTrue()
        {
            const string Supported = "did:ion:test:EiAaxkdLcU9e78H4gKmA61I1A3BtS01Lwr1Ed7N3Xoy7gQ?-ion-initial-state" + 
                                     "=eyJ0eXBlIjoiY3JlYXRlIiwic3VmZml4RGF0YSI6ImV5SndZWFJqYUVSaGRHRklZWE5vSWpvaVJXb" + 
                                     "EJiak5zZFRSZlZrVnVkMlZoU0c5VFlqRlpNM3AxVWtKaVNGRjRMVkZaVEZBM1FXdHFkRkYxY0ZwUVV" + 
                                     "TSXNJbkpsWTI5MlpYSjVTMlY1SWpwN0ltdDBlU0k2SWtWRElpd2lZM0oySWpvaWMyVmpjREkxTm1ze" + 
                                     "Elpd2llQ0k2SW1sS1ZHaHhibXRJVVV0eFdIcDZiMWxtZEUxVFJVeGZNRTFvWlV0SGEybERVelpWYTB" + 
                                     "kRWIwaDVjakFpTENKNUlqb2llR2h3TVdKd0xWbE1kbU5zYUhwSU9IaGhRWFZrYUhadWFEWkZaRVpPU" + 
                                     "jBwNmJYRmtWbHBVTmpKMVRTSjlMQ0p1WlhoMFVtVmpiM1psY25sRGIyMXRhWFJ0Wlc1MFNHRnphQ0k" + 
                                     "2SWtWcFFraGlabEZPT0ZwNk1sVkJlSEV3TFdsdFVIVk5lR2hHYUdWYVNIY3RWamRzTTFsM01IaEVUM" + 
                                     "EpZTkVFaWZRIiwicGF0Y2hEYXRhIjoiZXlKdVpYaDBWWEJrWVhSbFEyOXRiV2wwYldWdWRFaGhjMmd" + 
                                     "pT2lKRmFVSnNYM1pCY0hwSU5USmxTbU5MYTBkbFdYRkJWRmhyTFZReExXSjRhbDh6WkVkSE56UmtTM" + 
                                     "0ZVU20xUklpd2ljR0YwWTJobGN5STZXM3NpWVdOMGFXOXVJam9pY21Wd2JHRmpaU0lzSW1SdlkzVnR" + 
                                     "aVzUwSWpwN0luQjFZbXhwWTB0bGVYTWlPbHQ3SW1sa0lqb2ljMmxuYm1sdVowdGxlU0lzSW5SNWNHV" + 
                                     "WlPaUpUWldOd01qVTJhekZXWlhKcFptbGpZWFJwYjI1TFpYa3lNREU0SWl3aWFuZHJJanA3SW10MGV" + 
                                     "TSTZJa1ZESWl3aVkzSjJJam9pYzJWamNESTFObXN4SWl3aWVDSTZJa0ZGWVVGZlZFMXdUbk5TZDIxY" + 
                                     "VRuZGxOekI2TW5GZlpIb3hjbEUzUnpoblRqQmZWVUY1WkVWTmVWVWlMQ0o1SWpvaVNVTjZWalZEYVh" + 
                                     "GYVNtVkJVek0wZEVvMmREbEJkMHR2WlRWa1VYQnhiR1l5TlVWaGVUVlRkSEJqYnlKOWZWMHNJbk5sY" + 
                                     "25acFkyVkZibVJ3YjJsdWRITWlPbHQ3SW1sa0lqb2ljMlZ5ZG1salpVVnVaSEJ2YVc1MFNXUXhNak1" + 
                                     "pTENKMGVYQmxJam9pYzI5dFpWUjVjR1VpTENKelpYSjJhV05sUlc1a2NHOXBiblFpT2lKb2RIUndje" + 
                                     "m92TDNkM2R5NTFjbXd1WTI5dEluMWRmWDFkZlEifQ";

            Assert.IsTrue(Identifier.IsSupported(Supported));
        }

        /// <summary>
        /// Tests that true is returned by the method
        /// when a test ION identifier is passed.
        /// </summary>
        [TestMethod]
        public void IsSupported_TestInputSupported_ReturnTrue()
        {
            const string Supported = "did:ion:test:WRfXPg8dantKVubE3HX8pw?";
            Assert.IsTrue(Identifier.IsSupported(Supported));
        }
    }
}