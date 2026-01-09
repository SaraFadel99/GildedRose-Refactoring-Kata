using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    private const string CONJURED = "Conjured";

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }


    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name == SULFURAS)
            {
                continue;
            }
            applyQualityRulesBeforeSellInUpdate(i);
            updateSellIn(i);
            applyExpiredQualityRules(i);
        }
    }

    private void applyQualityRulesBeforeSellInUpdate(int i)
    {
        if (Items[i].Name == AGED_BRIE)
        {
            agedBrieQualityRules(i);
        }
        else if (Items[i].Name == BACKSTAGE_PASSES)
        {
            applyBackstagePassQualityRules(i);
        }
        else if (Items[i].Name == CONJURED)
        {
            applyQualityRulesOnConjured(i);
        }
        else
        {
            applyQualityRulesOnRegulars(i);
        }
    }

    private void applyExpiredQualityRules(int i)
    {
        if (Items[i].SellIn < 0)
        {
            if (Items[i].Name == AGED_BRIE)
            {
                agedBrieQualityRules(i);
            }
            else if (Items[i].Name == BACKSTAGE_PASSES)
            {
                Items[i].Quality = 0;
            }
            else if (Items[i].Name == CONJURED)
            {
                applyQualityRulesOnConjured(i);
            }
            else
            {
                applyQualityRulesOnRegulars(i);
            }
        }
    }

    private void updateSellIn(int i)
    {
        Items[i].SellIn = Items[i].SellIn - 1;
    }

    private void applyQualityRulesOnRegulars(int i)
    {
        if (Items[i].Quality > 0)
        {
            Items[i].Quality = Items[i].Quality - 1;
        }
    }
    private void applyQualityRulesOnConjured(int i)
    {
        if (Items[i].Quality > 0)
        {
            Items[i].Quality = Items[i].Quality - 2;
        }
    }
    private void agedBrieQualityRules(int i)
    {
        if (Items[i].Quality < 50)
        {
            Items[i].Quality = Items[i].Quality + 1;
        }
    }
    private void applyBackstagePassQualityRules(int i)
    {
        if (Items[i].Quality < 50)
        {
            if (Items[i].Name == BACKSTAGE_PASSES)
            {

                if (Items[i].SellIn < 11 && Items[i].SellIn > 5)
                {
                    int incrteasedQuality = Items[i].Quality + 2;
                    Items[i].Quality = incrteasedQuality < 50 ? incrteasedQuality : 50;
                }

                else if (Items[i].SellIn < 6)
                {
                    int incrteasedQuality = Items[i].Quality + 3;
                    Items[i].Quality = incrteasedQuality < 50 ? incrteasedQuality : 50;
                }
                else
                {
                    Items[i].Quality = Items[i].Quality + 1;
                }
            }
        }
 
    }

}