# RefIT

What is RefIT:
RefIT – Reference Interval Tool - is a standalone program, designed to help visualize population based laboratory datasets and calculate reference ranges. Although primarily designed for use with therapeutic drugs, it can also be applied on datasets from endogenous substances as well. The program takes input in the form of excel files, which can be obtained from most Laboratory Information Systems (LIMS). It has build-in export function of processed data in the same format, and in addition provides export of graphical views. Any calculations can easily be validated, by opening the export file in Excel.

What can it do:
The following is a list of features.
•	Batch import and calculation of multiple analytes from a single dataset file.
•	Automatically cleans data, and removes results “lower/higher than” on import.
•	Automatically verifies date of analysis on import.
•	Automatically identifies and anonymize patient ID’s when Danish social security numbers are used. 
•	Automatically calculates age and gender based on Danish social security number.
•	Checks for missing age and gender data when importing.
•	Provides four different models for selecting data when calculating reference ranges. 
•	Provides settings for selection of age group and gender when calculating reference ranges.
•	Provides settings for selection of time range of samples.
•	Provides selection of percentile range for calculating therapeutic reference ranges, and three modes of 
calculating the percentile.
•	Provides calculation of reference ranges based on -/+2 standard deviations.
•	Provides basic statistics like average, median, min/max.
•	Provides removal of outliers based on Tukey’s fences.
•	Provides visualization of all sample results by data plot, percentile plot and normal distribution plot.
•	Export of reports as excel file, or by printing.
•	Export of processed data as excel file.
•	Export of all graphical views as high resolution images. 

Comment on decimal number setting:
Numbering in the excel import file can be either “,” or “.” Please see attached demodata file for correct organization of data.

What algorithms do the program use:
RefIT provides algorithm‘s for selecting population based datasets. The models are described in the documentation, and in the paper “Automated inter-laboratory comparison of therapeutic drug monitoring data and its use for evaluation of published therapeutic reference ranges“ by Jens Borggaard Larsen et al. 2023 (in preparation).
The build in function for calculating percentiles are based on linear interpolation second and third variant (C=0, C=1) method as described https://en.wikipedia.org/wiki/Percentile#The_linear_interpolation_between_closest_ranks_method.
As the program use the datatype double for this calculation, there is a small difference when calculating, compared to excel, due to rounding. 
In addition RefIT has build in removal of outliers, based on Tukey’s fences (Interquartile range method). https://en.wikipedia.org/wiki/Outlier

Known bugs:
See documentation.

Distribution:
The RefIT v. 1.0 program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.
© Jens Borggaard Larsen 2023.

