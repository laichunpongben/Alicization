using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;

namespace Alicization.Model.Demographics.Players
{
    internal class Gene
    {
        internal double perception { get; private set; }
        internal double willpower { get; private set; }
        internal double intelligence { get; private set; }
        internal double memory { get; private set; }
        internal double charisma { get; private set; }

        internal Gene()
        {
            IList<double> uniforms = Randomness.SampleUniformSequence().Take(5).ToList();
            perception = uniforms[0];
            willpower = uniforms[1];
            intelligence = uniforms[2];
            memory = uniforms[3];
            charisma = uniforms[4];
        }

        internal Gene(Gene fatherGene, Gene motherGene)
        {
            IList<double> weights = Randomness.SampleUniformSequence().Take(5).ToList();

            perception = weights[0] * fatherGene.perception + (1 - weights[0]) * motherGene.perception;
            willpower =weights[1] * fatherGene.willpower + (1 - weights[1]) * motherGene.willpower;
            intelligence = weights[2] * fatherGene.intelligence + (1 - weights[2]) * motherGene.intelligence;
            memory = weights[3] * fatherGene.memory + (1 - weights[3]) * motherGene.memory;
            charisma = weights[4] * fatherGene.charisma + (1 - weights[4]) * motherGene.charisma;
        }

        internal Gene(Gene fatherGene, Gene motherGene, double weightingMutation)
        {
            IList<double> weights = Randomness.SampleUniformSequence().Take(5).ToList();
            IList<double> uniforms = Randomness.SampleUniformSequence().Take(5).ToList();

            perception = (1 - weightingMutation) * (weights[0] * fatherGene.perception + 
                                        (1 - weights[0]) * motherGene.perception) + weightingMutation * uniforms[0];
            willpower = (1 - weightingMutation) * (weights[1] * fatherGene.willpower + 
                                        (1 - weights[1]) * motherGene.willpower) + weightingMutation * uniforms[1];
            intelligence = (1 - weightingMutation) * (weights[2] * fatherGene.intelligence + 
                                        (1 - weights[2]) * motherGene.intelligence) + weightingMutation * uniforms[2];
            memory = (1 - weightingMutation) * (weights[3] * fatherGene.memory + 
                                        (1 - weights[3]) * motherGene.memory) + weightingMutation * uniforms[3];
            charisma = (1 - weightingMutation) * (weights[4] * fatherGene.charisma + 
                                        (1 - weights[4]) * motherGene.charisma) + weightingMutation * uniforms[4];
        }
    }
}
