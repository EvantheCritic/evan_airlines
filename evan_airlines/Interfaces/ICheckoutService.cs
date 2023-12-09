using evan_airlines.Models;
namespace evan_airlines.Interfaces;
public interface ICheckoutService
{
    List<FlightModel> GetFlightCart();
    void AddFlightToCart(FlightModel flight);
}

public class CheckoutService : ICheckoutService
{
    private List<FlightModel> flightCart = new List<FlightModel>();

    public List<FlightModel> GetFlightCart()
    {
        return flightCart;
    }

    public void AddFlightToCart(FlightModel flight)
    {
        flightCart.Add(flight);
    }
}
