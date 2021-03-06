/*<FILE_LICENSE>
* NFX (.NET Framework Extension) Unistack Library
* Copyright 2003-2014 IT Adapter Inc / 2015 Aum Code LLC
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
</FILE_LICENSE>*/


using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using NFX.Parsing;


namespace NFX.NUnit.Parsing
{
    [TestFixture]   
    public class Utils
    {
        [TestCase]
        public void FieldNameToDescription()
        {
          Assert.AreEqual("First Name", "FIRST_NAME".ParseFieldNameToDescription(true));
          Assert.AreEqual("first name", "FIRST_NAME".ParseFieldNameToDescription(false));

          Assert.AreEqual("First Name 2", "FIRST-NAME_2".ParseFieldNameToDescription(true));
          Assert.AreEqual("first name 2", "FIRST-NAME_2".ParseFieldNameToDescription(false));
        }


        [TestCase]
        public void MatchPattern1()
        {
          Assert.IsTrue( "some address".MatchPattern("s?me?addres?") );
          Assert.IsTrue( "same-addresZ".MatchPattern("s?me?addres?") );
        }

        [TestCase]
        public void MatchPattern2()
        {
          Assert.IsTrue ( "some address".MatchPattern("s?me?addres?", senseCase: true) );
          Assert.IsFalse( "same-addreZs".MatchPattern("s?me?addres?", senseCase: true) );
        }


        [TestCase]
        public void MatchPattern3()
        {
          Assert.IsTrue( "some address".MatchPattern("some*") );
        }
        [TestCase]
        public void MatchPattern4()
        {
          Assert.IsTrue( "some address".MatchPattern("s?me*") );
        }

        [TestCase]
        public void MatchPattern5()
        {
          Assert.IsTrue( "some address".MatchPattern("s?me*addre??") );
        }

        [TestCase]
        public void MatchPattern6()
        { 
          Assert.IsTrue( "same Address".MatchPattern("s?me*addre??") );
        } 

        [TestCase]
        public void MatchPattern7()
        { 
          Assert.IsTrue( "same AddreZZ".MatchPattern("s?me*addre??") );
          Assert.IsFalse( "same AddreZZ?".MatchPattern("s?me*addre??") );
          Assert.IsFalse( "same AddreZ"  .MatchPattern("s?me*addre??") );
        }

        [TestCase]
        public void MatchPattern8()
        { 
          Assert.IsTrue( "same AddreZZ".MatchPattern("*") );
        }

        [TestCase]
        public void MatchPattern9()
        { 
          Assert.IsFalse( "same AddreZZ".MatchPattern("") );
        }

        [TestCase]
        public void MatchPattern10()
        { 
          Assert.IsFalse( "same AddreZZ".MatchPattern("?") );
        }

        [TestCase]
        public void MatchPattern11()
        { 
          Assert.IsTrue( "same AddreZZ".MatchPattern("????????????") );
        }

        [TestCase]
        public void MatchPattern12()
        { 
          Assert.IsTrue( "same AddreZZ".MatchPattern("same*") );
        }

        [TestCase]
        public void MatchPattern13()
        { 
          Assert.IsTrue( "same AddreZZ".MatchPattern("*addre??") );
        }

        [TestCase]
        public void MatchPattern14()
        { 
          Assert.IsTrue( "same Address".MatchPattern("*address") );
        }

        [TestCase]
        public void MatchPattern15_1()
        { 
          Assert.IsTrue( "some same crazy address address Address".MatchPattern("*address") );
        }

        [TestCase]
        public void MatchPattern15_2()
        { 
          Assert.IsFalse( "some same crazy address address Address".MatchPattern("*address", senseCase: true) );
        }

        [ExpectedException(typeof(NFXException), ExpectedMessage="contains more than one", MatchType=MessageMatch.Contains)]
        [TestCase]
        public void MatchPattern16_1()
        { 
          "some crazy address".MatchPattern("*crazy*");
        }

        [ExpectedException(typeof(NFXException), ExpectedMessage="contains more than one", MatchType=MessageMatch.Contains)]
        [TestCase]
        public void MatchPattern16_2()
        { 
          "some crazy address".MatchPattern("cr*azy*");
        }


        [ExpectedException(typeof(NFXException), ExpectedMessage="contains more than one", MatchType=MessageMatch.Contains)]
        [TestCase]
        public void MatchPattern16_3()
        { 
          "some crazy address".MatchPattern("*cra*zy");
        }


        [TestCase]
        public void MatchPattern17()
        { 
          Assert.IsTrue( "127.0.0.1".MatchPattern("127.0.*") );
        }

        [TestCase]
        public void MatchPattern18()
        { 
          Assert.IsTrue( "https://some-site.com/?q=aaaa".MatchPattern("https://some-site.com*") );
        }

        [TestCase]
        public void MatchPattern19()
        { 
          Assert.IsTrue( "140.70.81.139".MatchPattern("140.70.81.139") );
        }

        [TestCase]
        public void MatchPattern20()
        { 
          Assert.IsTrue( "140.70.81.139" .MatchPattern("140.70.*.139") );
          Assert.IsTrue( "140.70.1.139"  .MatchPattern("140.70.*.139") );
          Assert.IsTrue( "140.70.17.139" .MatchPattern("140.70.*.139") );
          Assert.IsTrue( "140.70.123.139".MatchPattern("140.70.*.139") );

          Assert.IsFalse( "141.70.81.139" .MatchPattern("140.70.*.139") );
          Assert.IsFalse( "140.71.1.139"  .MatchPattern("140.70.*.139") );
          Assert.IsFalse( "140.70.17.13"  .MatchPattern("140.70.*.139") );
          Assert.IsFalse( "140.70.123.137".MatchPattern("140.70.*.139") );
        }

        [TestCase]
        public void CheckScreenName()
        { 
          Assert.IsFalse( DataEntryUtils.CheckScreenName("10o") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName("1.0o") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName(".aa") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName("2d-2222") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName("DIMA-aaaaa..") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName("дима 123") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName(".дима 123") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName("1дима-123") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName("-дима") );
          Assert.IsFalse( DataEntryUtils.CheckScreenName("дима.") );


          Assert.IsTrue(  DataEntryUtils.CheckScreenName("dima-qwerty") );
          Assert.IsTrue(  DataEntryUtils.CheckScreenName("d2-2222") );
          Assert.IsTrue( DataEntryUtils.CheckScreenName("дима123") );
          Assert.IsTrue( DataEntryUtils.CheckScreenName("дима-123") );
          Assert.IsTrue( DataEntryUtils.CheckScreenName("дима.123") );
        }

        [TestCase]
        public void NormalizePhone1()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("5552224415");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415", n);
        }

        [TestCase]
        public void NormalizePhone2()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("2224415");
          Console.WriteLine(n);
          Assert.AreEqual("(???) 222-4415", n);
        }

        [TestCase]
        public void NormalizePhone3()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("   +38 067 2148899   ");
          Console.WriteLine(n);
          Assert.AreEqual("+38 067 2148899", n);
        }

        [TestCase]
        public void NormalizePhone4()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("555-222-4415");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415", n);
        }

        [TestCase]
        public void NormalizePhone5()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("555-222-4415 EXT 2014");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415x2014", n);
        }

        [TestCase]
        public void NormalizePhone6()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("555-222-4415.2014");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415x2014", n);
        }

        [TestCase]
        public void NormalizePhone7()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("555-222-4415EXT.2014");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415x2014", n);
        }

        [TestCase]
        public void NormalizePhone8()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("555-222-4415 X 2014");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415x2014", n);
        }

        [TestCase]
        public void NormalizePhone9()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("555.222.4415");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415", n);
        }

        [TestCase]
        public void NormalizePhone10()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("555-222-4415");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415", n);
        }

        [TestCase]
        public void NormalizePhone11()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("5552224415ext123");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415x123", n);
        }

        [TestCase]
        public void NormalizePhone12()
        { 
          var n = DataEntryUtils.NormalizeUSPhone("5552224415ext.123");
          Console.WriteLine(n);
          Assert.AreEqual("(555) 222-4415x123", n);
        }


    }
}


