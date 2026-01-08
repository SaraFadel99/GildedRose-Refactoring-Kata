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
            if (Items[i].Name == "Sulfuras, Hand of Ragnaros")
            {
                continue;
            }
            if (Items[i].Name == "Aged Brie" || Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                applyQualityRulesForBrieAndBackstagePass(i);
            }
            else
            {
                applyQualityRulesOnRegulars(i);
            }
     
            applyExpiredQualityRules(i);
        }
    }

    private void applyQualityRulesForBrieAndBackstagePass(int i)
    {
        if (Items[i].Quality < 50)
        {
            if (Items[i].Name == "Aged Brie") 
            {
                expiredAgedBriedQualityRules(i);
                // Items[i].Quality = Items[i].Quality + 1;

            }
            else 
            {
                applyBackstagePassQualityRules(i);
            }

        }
    }

    private void applyBackstagePassQualityRules(int i)
    {
        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
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

    private void applyQualityRulesOnRegulars(int i)
    {
        if (Items[i].Quality > 0)
        {
           Items[i].Quality = Items[i].Quality - 1;
        }
    }

    private void applyExpiredQualityRules(int i)
    {
        Items[i].SellIn = Items[i].SellIn - 1;
        if (Items[i].SellIn < 0)
        {
            if (Items[i].Name == "Aged Brie")
            {
                expiredAgedBriedQualityRules(i);
            }

            else if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                Items[i].Quality = 0;
            }
            else
            {
                regularsExpiredQualityRules(i);
            }
            }
        
    }

    private void regularsExpiredQualityRules(int i)
    {
        if (Items[i].Quality > 0)
        {
            Items[i].Quality = Items[i].Quality - 1;
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