import React from 'react';
import CraftingComponent from './CraftComponent';
import PricesComponent from './PriceComponent';
import ItemsComponent from './ItemComponent';
import RecipesComponent from './RecipeComponent';

interface ResultsProps {
    componentName: string;
    data: any;
}

const ResultsComponent: React.FC<ResultsProps> = ({ componentName, data }) => {
    switch (componentName) {
        case 'Items':
            return <ItemsComponent item={data} />;
        case 'Recipes':
            return <RecipesComponent recipe={data} />;
        case 'Prices':
            return <PricesComponent price={data} />;
        case 'Crafting':
            return <CraftingComponent craftSummary={data} />;
        default:
            return null;
    }
};

export default ResultsComponent;