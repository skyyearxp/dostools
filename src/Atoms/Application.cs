﻿namespace Vurdalakov
{
    using System;

    public class Application : DosToolsApplication
    {
        protected override Int32 Execute()
        {
            if (_commandLineParser.IsOptionSet("ga"))
            {
                var name = GetName("ga");
                var atom = AtomTable.GlobalAdd(name);
                Console.WriteLine("{0}={1}", name, atom);
            }
            else if (_commandLineParser.IsOptionSet("gd"))
            {
                var atom = GetAtom("gd");
                AtomTable.GlobalDelete(atom);
            }
            else if (_commandLineParser.IsOptionSet("gf"))
            {
                var name = GetName("gf");
                var atom = AtomTable.GlobalFind(name);
                Console.WriteLine("{0}={1}", name, 0 == atom ? "<Not found>" : atom.ToString());
            }
            else if (_commandLineParser.IsOptionSet("gq"))
            {
                var atom = GetAtom("gq");
                var name = AtomTable.GlobalGetName(atom);
                Console.WriteLine("{0}={1}", atom, null == name ? "<Not found>" : name);
            }
            else if (_commandLineParser.IsOptionSet("ge"))
            {
                for (var atom = 0xC000; atom <= 0xFFFF; atom++)
                {
                    var name = AtomTable.GlobalGetName((UInt16)atom);
                    if (name != null)
                    {
                        Console.WriteLine("{0}\t0x{0:X4}\t{1}", atom, name);
                    }
                }
            }
            else if (_commandLineParser.IsOptionSet("na"))
            {
                var name = GetName("na");
                var atom = AtomTable.NtAdd(name);
                Console.WriteLine("{0}={1}", name, atom);
            }
            else if (_commandLineParser.IsOptionSet("nd"))
            {
                var atom = GetAtom("nd");
                AtomTable.NtDelete(atom);
            }
            else if (_commandLineParser.IsOptionSet("nf"))
            {
                var name = GetName("nf");
                var atom = AtomTable.NtFind(name);
                Console.WriteLine("{0}={1}", name, 0 == atom ? "<Not found>" : atom.ToString());
            }
            else if (_commandLineParser.IsOptionSet("nq"))
            {
                var atom = GetAtom("nq");
                var atomBasicInformation = AtomTable.NtQueryBasicInformation(atom);
                if (null == atomBasicInformation)
                {
                    Console.WriteLine("<Not found>");
                }
                else
                {
                    Console.WriteLine("Atom={0}", atomBasicInformation.Atom);
                    Console.WriteLine("ReferenceCount={0}", atomBasicInformation.ReferenceCount);
                    Console.WriteLine("Pinned={0}", atomBasicInformation.Pinned);
                    Console.WriteLine("Name={0}", atomBasicInformation.Name);
                }
            }
            else if (_commandLineParser.IsOptionSet("ne"))
            {
                for (var atom = 0xC000; atom <= 0xFFFF; atom++)
                {
                    var info = AtomTable.NtQueryBasicInformation((UInt16)atom);
                    if (info != null)
                    {
                        Console.WriteLine("{0}\t0x{0:X4}\t{1}\t{2}", atom, info.ReferenceCount, info.Name);
                    }
                }
            }
            else if (_commandLineParser.IsOptionSet("ua"))
            {
                var name = GetName("ua");
                var atom = AtomTable.UserAdd(name);
                Console.WriteLine("{0}={1}", name, atom);
            }
            else if (_commandLineParser.IsOptionSet("uf"))
            {
                var name = GetName("uf");
                var atom = 0;
                for (var atom1 = 0xC000; atom1 <= 0xFFFF; atom1++)
                {
                    var name1 = AtomTable.UserGetName((UInt16)atom1);
                    if ((name1 != null) && name.Equals(name1, StringComparison.CurrentCultureIgnoreCase))
                    {
                        atom = atom1;
                        break;
                    }
                }
                Console.WriteLine("{0}={1}", name, 0 == atom ? "<Not found>" : atom.ToString());
            }
            else if (_commandLineParser.IsOptionSet("uq"))
            {
                var atom = GetAtom("uq");
                var name = AtomTable.UserGetName(atom);
                Console.WriteLine("{0}={1}", atom, null == name ? "<Not found>" : name);
            }
            else if (_commandLineParser.IsOptionSet("ue"))
            {
                for (var atom = 0xC000; atom <= 0xFFFF; atom++)
                {
                    var name = AtomTable.UserGetName((UInt16)atom);
                    if (name != null)
                    {
                        Console.WriteLine("{0}\t0x{0:X4}\t{1}", atom, name);
                    }
                }
            }
            else if (_commandLineParser.IsOptionSet("la"))
            {
                var name = GetName("la");
                var atom = AtomTable.LocalAdd(name);
                Console.WriteLine("{0}={1}", name, atom);
            }
            else if (_commandLineParser.IsOptionSet("ld"))
            {
                var atom = GetAtom("ld");
                AtomTable.LocalDelete(atom);
            }
            else if (_commandLineParser.IsOptionSet("lf"))
            {
                var name = GetName("lf");
                var atom = AtomTable.LocalFind(name);
                Console.WriteLine("{0}={1}", name, 0 == atom ? "<Not found>" : atom.ToString());
            }
            else if (_commandLineParser.IsOptionSet("lq"))
            {
                var atom = GetAtom("lq");
                var name = AtomTable.LocalGetName(atom);
                Console.WriteLine("{0}={1}", atom, null == name ? "<Not found>" : name);
            }
            else if (_commandLineParser.IsOptionSet("le"))
            {
                for (var atom = 0xC000; atom <= 0xFFFF; atom++)
                {
                    var name = AtomTable.LocalGetName((UInt16)atom);
                    if (name != null)
                    {
                        Console.WriteLine("{0}\t0x{0:X4}\t{1}", atom, name);
                    }
                }
            }
            //else if (_commandLineParser.IsOptionSet("lt"))
            //{
            //    // TEST

            //    // ADD
            //    var atom = AtomTable.LocalAdd("TEST");
            //    Console.WriteLine("{0}={1}", "TEST", atom);

            //    // FIND
            //    atom = AtomTable.LocalFind("TEST");
            //    Console.WriteLine("{0}={1}", "TEST", 0 == atom ? "<Not found>" : atom.ToString());

            //    // QUERY
            //    var name = AtomTable.LocalGetName(atom);
            //    Console.WriteLine("{0}={1}", atom, null == name ? "<Not found>" : name);

            //    // ENUMRATE
            //    for (var _atom = 0xC000; _atom <= 0xFFFF; _atom++)
            //    {
            //        var _name = AtomTable.LocalGetName((UInt16)_atom);
            //        if (_name != null)
            //        {
            //            Console.WriteLine("{0}\t0x{0:X4}\t{1}", _atom, _name);
            //        }
            //    }

            //    // DELETE
            //    AtomTable.LocalDelete(atom);

            //    // ENUMRATE(part 2)
            //    for (var _atom = 0xC000; _atom <= 0xFFFF; _atom++)
            //    {
            //        var _name = AtomTable.LocalGetName((UInt16)_atom);
            //        if (_name != null)
            //        {
            //            Console.WriteLine("{0}\t0x{0:X4}\t{1}", _atom, _name);
            //        }
            //    }
            //}
            else
            {
                Help();
            }

            return 0;
        }

        private String GetName(String optionName)
        {
            var name = _commandLineParser.GetOptionString(optionName, null);
            if (null == name)
            {
                Help();
            }
            return name;
        }

        private UInt16 GetAtom(String optionName)
        {
            var atom = _commandLineParser.GetOptionInt(optionName, -1);
            if ((atom < 0) || (atom > UInt16.MaxValue))
            {
                Help();
            }
            return (UInt16)atom;
        }

        protected override void Help()
        {
            Console.WriteLine("Atoms {0} | https://github.com/vurdalakov/dostools\n", ApplicationVersion);
            Console.WriteLine("Works with Windows global and user atom tables.\n");
            Console.WriteLine("Usage:\n\tatoms <-command[:parameter]> [-silent]\n");
            Console.WriteLine("Commands:");
            Console.WriteLine("\tGlobal atom table:  -ga:string, -gd:atom, -gf:string, -gq:atom, -ge");
            Console.WriteLine("\tSame through NTDLL: -na:string, -nd:atom, -nf:string, -nq:atom, -ne");
            Console.WriteLine("\tUser atom table:    -ua:string,           -uf:string, -uq:atom, -ue");
            Console.WriteLine("\tLocal atom table:   -la:string, -ld:atom, -lf:string, -lq:atom, -le\n");
            Console.WriteLine("\tSecond letter in command: a=Add, d=Delete, f=Find, q=Query, e=Enumerate\n");
            Console.WriteLine("Options:\n\t-silent - no error messages are shown; check exit code\n");
            Console.WriteLine("Exit codes:\n\t0 - command succeeded\n\t1 - command failed\n\t-1 - invalid command line syntax\n");

            base.Help();
        }
    }
}
