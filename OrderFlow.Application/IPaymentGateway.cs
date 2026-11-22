namespace OrderFlow.Application;

public interface IPaymentGateway
{
    bool ChargeCard(decimal orderTotal);

    bool ChargePayPal(decimal orderTotal);

    bool ChargeUpi(decimal orderTotal);
}
