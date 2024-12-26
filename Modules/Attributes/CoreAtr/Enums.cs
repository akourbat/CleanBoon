using System.Reactive.Linq;
using DynamicData.Binding;

namespace Attributes;

public enum ATR {VITALITY, TOUGHNESS, POWER, PRECISION, CONDI_DMG, EXPERTISE, CONCENTRATION, SPEED};
public enum TALENT_TYPE {ATR, ABILITY}
public record TalentAtr( ATR Atr, int Value);

public abstract class TalentBase: AbstractNotifyPropertyChanged 
{   
    public string ID {get; init;}
    public TALENT_TYPE Type {get; init;} // Maybe not needed
}
public class TalentOutcome: TalentBase, IDisposable
{
    private TalentAtr _value;
    public TalentAtr Value { get => _value; set => SetAndRaise(ref _value, value); }
    
    public TalentOutcome(string Id, IObservable<TalentAtr> obs)
    {       
        this.ID = Id;
        this.Type = TALENT_TYPE.ATR;
        this.sub = obs.Subscribe(x => this.Value = x);
    }
    IDisposable sub;
    public void Dispose() => sub.Dispose();
}

