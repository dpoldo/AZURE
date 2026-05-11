using Xunit;

/// <summary>
/// Tests for the MyProperty Int32 property added to the Dummy class.
/// </summary>
public class DummyTests
{
    [Fact]
    public void MyProperty_DefaultValue_IsZero()
    {
        var dummy = new Dummy();

        Assert.Equal(0, dummy.MyProperty);
    }

    [Fact]
    public void MyProperty_SetAndGet_ReturnsAssignedValue()
    {
        var dummy = new Dummy();

        dummy.MyProperty = 42;

        Assert.Equal(42, dummy.MyProperty);
    }

    [Fact]
    public void MyProperty_SetToMaxInt32_ReturnsMaxValue()
    {
        var dummy = new Dummy();

        dummy.MyProperty = Int32.MaxValue;

        Assert.Equal(Int32.MaxValue, dummy.MyProperty);
    }

    [Fact]
    public void MyProperty_SetToMinInt32_ReturnsMinValue()
    {
        var dummy = new Dummy();

        dummy.MyProperty = Int32.MinValue;

        Assert.Equal(Int32.MinValue, dummy.MyProperty);
    }

    [Fact]
    public void MyProperty_SetToNegativeValue_ReturnsNegativeValue()
    {
        var dummy = new Dummy();

        dummy.MyProperty = -100;

        Assert.Equal(-100, dummy.MyProperty);
    }

    [Fact]
    public void MyProperty_SetToZeroExplicitly_ReturnsZero()
    {
        var dummy = new Dummy();
        dummy.MyProperty = 99;

        dummy.MyProperty = 0;

        Assert.Equal(0, dummy.MyProperty);
    }

    [Fact]
    public void MyProperty_OverwrittenValue_ReturnsLatestValue()
    {
        var dummy = new Dummy();
        dummy.MyProperty = 10;

        dummy.MyProperty = 20;

        Assert.Equal(20, dummy.MyProperty);
    }

    [Fact]
    public void MyProperty_IsIndependentAcrossInstances()
    {
        var dummy1 = new Dummy();
        var dummy2 = new Dummy();

        dummy1.MyProperty = 1;
        dummy2.MyProperty = 2;

        Assert.Equal(1, dummy1.MyProperty);
        Assert.Equal(2, dummy2.MyProperty);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(100)]
    [InlineData(-100)]
    [InlineData(Int32.MaxValue)]
    [InlineData(Int32.MinValue)]
    public void MyProperty_SetVariousValues_ReturnsExpectedValue(int value)
    {
        var dummy = new Dummy();

        dummy.MyProperty = value;

        Assert.Equal(value, dummy.MyProperty);
    }
}
