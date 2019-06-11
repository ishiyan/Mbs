import { Category } from './category';
import { exampleFormulasSamples } from '../samples/example-formulas-samples';
import { basicFunctionalitySamples } from '../samples/basic-functionality-samples';
import { multilineFunctionalitySamples } from '../samples/multiline-functionality-samples';
import { symbolSamples } from '../samples/symbol-samples';
import { scienceEquationsSamples } from '../samples/science-equations-samples';

export const categories: Category[] = [

    {
        name: 'Example formulas', samples: exampleFormulasSamples
    },
    {
        name: 'Basic functionality', samples: basicFunctionalitySamples
    },
    {
        name: 'Multiline functionality', samples: multilineFunctionalitySamples
    },
    {
        name: 'Symbols', samples: symbolSamples
    },
    {
        name: 'Science equations', samples: scienceEquationsSamples
    }
];
