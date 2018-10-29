public interface IPayReceiverListener
{
    //-------------------------------------------------------------------------
    void PayResultIPA(_ePayOptionType option_type, bool is_success, object result);

    //-------------------------------------------------------------------------
    void PayResult(string result);
}
