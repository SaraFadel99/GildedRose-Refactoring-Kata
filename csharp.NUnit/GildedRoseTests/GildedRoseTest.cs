using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Test]
    public void RegularItem_QualityDecreasesByOne_BeforeSellIn()
    {
        var items = new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 }
        };
        var app = new GildedRose(items);

        app.UpdateQuality();

        Assert.That(items[0].Quality, Is.EqualTo(19), "Quality should decrease by 1");
        Assert.That(items[0].SellIn, Is.EqualTo(9), "SellIn should decrease by 1");
    }

    [Test]
    public void RegularItem_QualityNeverGoesBelowZero()
    {
        var items = new List<Item>
        {
            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 0 }
        };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(0), "Quality should never go below 0");
    }

    [Test]
    public void RegularItem_QualityDecreasesByTwo_AfterSellIn()
    {
        var items = new List<Item>
        {
            new Item { Name = "Elixir of the Mongoose", SellIn = 0, Quality = 10 }
        };
        var app = new GildedRose(items);

        app.UpdateQuality();

        Assert.That(items[0].Quality, Is.EqualTo(8), "Quality should decrease by 2 after SellIn date (0 -> -1)");
        Assert.That(items[0].SellIn, Is.EqualTo(-1), "SellIn should be negative after passing sell date");
    }

    // Aged Brie Tests
    [Test]
    public void AgedBrie_QualityIncreasesByOne_BeforeSellIn()
    {
        var items = new List<Item>
        {
            new Item { Name = "Aged Brie", SellIn = 5, Quality = 10 }
        };
        var app = new GildedRose(items);

        app.UpdateQuality();

        Assert.That(items[0].Quality, Is.EqualTo(11), "Aged Brie quality should increase by 1 before SellIn date");
        Assert.That(items[0].SellIn, Is.EqualTo(4), "SellIn should decrease by 1");
    }

    [Test]
    public void AgedBrie_QualityNeverExceedsFifty()
    {
        var items = new List<Item>
        {
            new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 }
        };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(50), "Aged Brie quality should never exceed 50");
    }

    [Test]
    public void AgedBrie_QualityIncreasesByTwo_AfterSellIn()
    {
        var items = new List<Item>
        {
            new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 }
        };
        var app = new GildedRose(items);

        app.UpdateQuality();

        Assert.That(items[0].Quality, Is.EqualTo(12), "Aged Brie quality should increase by 2 after SellIn date (0 -> -1)");
        Assert.That(items[0].SellIn, Is.EqualTo(-1), "SellIn should be negative after passing sell date");
    }

    [Test]
    public void AgedBrie_QualityNeverExceedsFifty_AfterSellIn()
    {
        var items = new List<Item>
        {
            new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 }
        };
        var app = new GildedRose(items);

        app.UpdateQuality();

        Assert.That(items[0].Quality, Is.EqualTo(50), "Aged Brie quality should cap at 50 even when increasing by 2");
    }

    // Backstage Passes Tests
    [Test]
    public void BackstagePass_AllScenarios_CoversAllCases()
    {
        var items = new List<Item>
        {
            // Quality increases by 1 when SellIn > 10
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 20 },
        
            // Quality increases by 2 when SellIn <= 10
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 6, Quality = 20 },
        
            // Quality increases by 3 when SellIn <= 5
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = 20 },
        
            // Quality drops to 0 after SellIn
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 30 },
        
            // Quality never exceeds 50
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 50 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 48 }
        };

        var app = new GildedRose(items);
        app.UpdateQuality();

        // Assert - Quality increases by 1 when SellIn > 10
        Assert.That(items[0].Quality, Is.EqualTo(21), "Backstage pass quality should increase by 1 when SellIn > 10");
        Assert.That(items[0].SellIn, Is.EqualTo(14), "SellIn should decrease by 1");

        Assert.That(items[1].Quality, Is.EqualTo(21), "Backstage pass quality should increase by 1 when SellIn = 11");
        Assert.That(items[1].SellIn, Is.EqualTo(10), "SellIn should decrease by 1");

        // Assert - Quality increases by 2 when SellIn <= 10
        Assert.That(items[2].Quality, Is.EqualTo(22), "Backstage pass quality should increase by 2 when SellIn <= 10");
        Assert.That(items[2].SellIn, Is.EqualTo(9), "SellIn should decrease by 1");

        Assert.That(items[3].Quality, Is.EqualTo(22), "Backstage pass quality should increase by 2 when SellIn = 6");
        Assert.That(items[3].SellIn, Is.EqualTo(5), "SellIn should decrease by 1");

        // Assert - Quality increases by 3 when SellIn <= 5
        Assert.That(items[4].Quality, Is.EqualTo(23), "Backstage pass quality should increase by 3 when SellIn <= 5");
        Assert.That(items[4].SellIn, Is.EqualTo(4), "SellIn should decrease by 1");

        Assert.That(items[5].Quality, Is.EqualTo(23), "Backstage pass quality should increase by 3 when SellIn = 1");
        Assert.That(items[5].SellIn, Is.EqualTo(0), "SellIn should decrease by 1");

        // Assert - Quality drops to 0 after SellIn
        Assert.That(items[6].Quality, Is.EqualTo(0), "Backstage pass quality should drop to 0 at sell date");
        Assert.That(items[6].SellIn, Is.EqualTo(-1), "SellIn should be negative after sell date");

        Assert.That(items[7].Quality, Is.EqualTo(0), "Backstage pass quality should drop to 0 after sell date");
        Assert.That(items[7].SellIn, Is.EqualTo(-2), "SellIn should continue decreasing");

        // Assert - Quality never exceeds 50
        Assert.That(items[8].Quality, Is.EqualTo(50), "Backstage pass quality should never exceed 50");
        Assert.That(items[8].SellIn, Is.EqualTo(14), "SellIn should decrease by 1");

        Assert.That(items[9].Quality, Is.EqualTo(50), "Backstage pass quality should cap at 50 even when increasing by 2");
        Assert.That(items[9].SellIn, Is.EqualTo(9), "SellIn should decrease by 1");

        Assert.That(items[10].Quality, Is.EqualTo(50), "Backstage pass quality should cap at 50 even when increasing by 3");
        Assert.That(items[10].SellIn, Is.EqualTo(4), "SellIn should decrease by 1");
    }

    // Sulfuras Tests
    [Test]
    public void Sulfuras_QualityAndSellInNeverChanges()
    {
        var items = new List<Item>
        {
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 }
        };

        var app = new GildedRose(items);
        app.UpdateQuality();

        // Assert - Quality never changes
        Assert.That(items[0].Quality, Is.EqualTo(80), "Sulfuras quality should never change at sell date");
        Assert.That(items[1].Quality, Is.EqualTo(80), "Sulfuras quality should never change after sell date");
        Assert.That(items[2].Quality, Is.EqualTo(80), "Sulfuras quality should never change before sell date");

        // Assert - SellIn never changes
        Assert.That(items[0].SellIn, Is.EqualTo(0), "Sulfuras SellIn should never change at sell date");
        Assert.That(items[1].SellIn, Is.EqualTo(-1), "Sulfuras SellIn should never change after sell date");
        Assert.That(items[2].SellIn, Is.EqualTo(5), "Sulfuras SellIn should never change before sell date");
    }

    // Edge Cases
    [Test]
    public void MultipleItems_AllUpdatedCorrectly()
    {
        var items = new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 }
        };

        var app = new GildedRose(items);
        app.UpdateQuality();

        // Assert - Verify all items are updated correctly
        Assert.That(items[0].Quality, Is.EqualTo(19), "Regular item should decrease by 1");
        Assert.That(items[0].SellIn, Is.EqualTo(9), "Regular item SellIn should decrease");

        Assert.That(items[1].Quality, Is.EqualTo(1), "Aged Brie should increase by 1");
        Assert.That(items[1].SellIn, Is.EqualTo(1), "Aged Brie SellIn should decrease");

        Assert.That(items[2].Quality, Is.EqualTo(6), "Regular item should decrease by 1");
        Assert.That(items[2].SellIn, Is.EqualTo(4), "Regular item SellIn should decrease");

        Assert.That(items[3].Quality, Is.EqualTo(80), "Sulfuras quality should never change");
        Assert.That(items[3].SellIn, Is.EqualTo(0), "Sulfuras SellIn should never change");

        Assert.That(items[4].Quality, Is.EqualTo(21), "Backstage pass should increase by 1 when SellIn > 10");
        Assert.That(items[4].SellIn, Is.EqualTo(14), "Backstage pass SellIn should decrease");

        Assert.That(items[5].Quality, Is.EqualTo(50), "Backstage pass should cap at 50");
        Assert.That(items[5].SellIn, Is.EqualTo(9), "Backstage pass SellIn should decrease");

        Assert.That(items[6].Quality, Is.EqualTo(23), "Backstage pass should increase by 3 when SellIn <= 5");
        Assert.That(items[6].SellIn, Is.EqualTo(4), "Backstage pass SellIn should decrease");

        Assert.That(items[7].Quality, Is.EqualTo(0), "Backstage pass should drop to 0 after sell date");
        Assert.That(items[7].SellIn, Is.EqualTo(-1), "Backstage pass SellIn should be negative");
    }

    [Test]
    public void EmptyList_NoErrors()
    {
        var items = new List<Item>();
        var app = new GildedRose(items);

        Assert.DoesNotThrow(() => app.UpdateQuality(), "UpdateQuality should handle empty list without errors");
        Assert.That(items.Count, Is.EqualTo(0), "Empty list should remain empty");
    }



    [Test]
    public void ConjuiredItems_tests()
    {
        var items = new List<Item>
        {
            new Item { Name = "Conjured", SellIn = 5, Quality = 20 },
            new Item { Name = "Conjured", SellIn = 10, Quality = 50 },
            new Item { Name = "Conjured", SellIn = 0, Quality = 10 },
            new Item { Name = "Conjured", SellIn = 7, Quality = 15 },
            new Item { Name = "Conjured", SellIn = 1, Quality = 25 },
            new Item { Name = "Conjured", SellIn = -1, Quality = 10 },
            new Item { Name = "Conjured", SellIn = 5, Quality = 2 }


        };
        var app = new GildedRose(items);

        app.UpdateQuality();

        Assert.That(items[0].Quality, Is.EqualTo(18));
        Assert.That(items[0].SellIn, Is.EqualTo(4));

        Assert.That(items[1].Quality, Is.EqualTo(48));
        Assert.That(items[1].SellIn, Is.EqualTo(9));

        Assert.That(items[2].Quality, Is.EqualTo(6));
        Assert.That(items[2].SellIn, Is.EqualTo(-1));

        Assert.That(items[3].Quality, Is.EqualTo(13));
        Assert.That(items[3].SellIn, Is.EqualTo(6));

        Assert.That(items[4].Quality, Is.EqualTo(23));
        Assert.That(items[4].SellIn, Is.EqualTo(0));

        Assert.That(items[5].Quality, Is.EqualTo(6));
        Assert.That(items[5].SellIn, Is.EqualTo(-2));

        Assert.That(items[6].Quality, Is.EqualTo(0));
        Assert.That(items[6].SellIn, Is.EqualTo(4));

    }
}