namespace FluxoPedidos.Micro.Domain.Base
{
    public static class DecimalExt
    {
        public static decimal Monetario(this decimal valor)
        {
            return Math.Round(valor, 2, MidpointRounding.AwayFromZero);
        }
    }
}
