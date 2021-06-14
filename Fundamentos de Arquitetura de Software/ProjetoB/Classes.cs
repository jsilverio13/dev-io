using ProjetoA;

namespace ProjetoB
{
    internal class TesteClasses
    {
        public TesteClasses()
        {
            var publica = new Publica();
            //var privada = new Privada();
            //var interna = new Interna();
            //var abstrata = new Abstrata();
        }
    }

    internal class TesteModificador1
    {
        public TesteModificador1()
        {
            var publica = new Publica();
            publica.TestePublico();
            //publica.TesteInternal();
            //publica.TesteProtegidoInterno();
            //publica.TesteProtegido();
            //publica.TestePrivadoProtegido();
            //publica.TestePrivado();
        }
    }

    internal class TesteModificador2 : Publica
    {
        public TesteModificador2()
        {
            TestePublico();
            TesteProtegido();
            TesteProtegidoInterno();
            //TesteInternal();
            //TestePrivadoProtegido();
            //TestePrivado();
        }
    }
}