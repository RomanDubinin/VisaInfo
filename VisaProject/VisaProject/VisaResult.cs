namespace VisaProject
{
    public enum VisaResult
    {
        None=0,      //Nenalezeno
        InService=1, //Zpracovává se
        Failure=2,   //Vyřízeno – NEPOVOLENO
        Success=3,   //Vyřízeno – POVOLENO
    }
}