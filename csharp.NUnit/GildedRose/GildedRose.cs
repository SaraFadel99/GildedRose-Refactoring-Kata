using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                applyQualityRulesOnRegulars(i);
            }
            else
            {
                applyQualityRulesForBrieAndBackstagePass(i);
            }

            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
                Items[i].SellIn = Items[i].SellIn - 1;
            }
            applyExpiredQualityRules(i);
        }
    }

    private void applyQualityRulesForBrieAndBackstagePass(int i)
    {
        if (Items[i].Quality < 50)
        {
            Items[i].Quality = Items[i].Quality + 1;

            applyBackstagePassQualityRules(i);
        }
    }

    private void applyBackstagePassQualityRules(int i)
    {
        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
        {
            if (Items[i].SellIn < 11)
            {
                if (Items[i].Quality < 50)
                {
                    Items[i].Quality = Items[i].Quality + 1;
                }
            }

            if (Items[i].SellIn < 6)
            {
                if (Items[i].Quality < 50)
                {
                    Items[i].Quality = Items[i].Quality + 1;
                }
            }
        }
    }

    private void applyQualityRulesOnRegulars(int i)
    {
        if (Items[i].Quality > 0)
        {
            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
                Items[i].Quality = Items[i].Quality - 1;
            }
        }
    }

    private void applyExpiredQualityRules(int i)
    {
        if (Items[i].SellIn < 0)
        {
            if (Items[i].Name != "Aged Brie")
            {
                if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    Items[i].Quality = Items[i].Quality - Items[i].Quality;
                }
                else
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
            }
            else
            {
                expiredAgedBriedQualityRules(i);
            }
        }
    }

    private void expiredAgedBriedQualityRules(int i)
    {
        if (Items[i].Quality < 50)
        {
            Items[i].Quality = Items[i].Quality + 1;
        }
    }
}