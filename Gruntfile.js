module.exports = function(grunt) {

	var path = require('path');

	// Load the package JSON file
	var pkg = grunt.file.readJSON('package.json');

	// get the root path of the project
	var projectRoot = 'src/' + pkg.name + '/';

	// Load information about the assembly
	var assembly = grunt.file.readJSON(projectRoot + 'Properties/AssemblyInfo.json');

	// Get the version of the package
	var version = assembly.informationalVersion ? assembly.informationalVersion : assembly.version;

	grunt.initConfig({
		pkg: pkg,
		clean: {
			files: [
				'releases/temp/'
			]
		},
		copy: {
			bacon: {
				files: [
					{
						expand: true,
						cwd: projectRoot + 'bin/Release/',
						src: [
							'Skybrud.Essentials.dll',
							'Skybrud.Essentials.xml',
							pkg.name + '.dll',
							pkg.name + '.xml'
						],
						dest: 'releases/temp/bin/'
					}
				]
			}
		},
		zip: {
			release: {
				cwd: 'releases/temp/',
				src: [
					'releases/temp/**/*.*'
				],
				dest: 'releases/github/' + pkg.name + '.v' + version + '.zip'
			}
		},
		umbracoPackage: {
			dist: {
				src: 'releases/temp/',
				dest: 'releases/umbraco',
				options: {
					name: pkg.name,
					version: version,
					url: pkg.url,
					license: pkg.license.name,
					licenseUrl: pkg.license.url,
					author: pkg.author.name,
					authorUrl: pkg.author.url,
					readme: pkg.readme,
					outputName: pkg.name + '.v' + version + '.zip'
				}
			}
		},
		nugetpack: {
			dist: {
				src: 'src/' + pkg.name + '/' + pkg.name + '.csproj',
				dest: 'releases/nuget/'
			}
		}
	});

	grunt.loadNpmTasks('grunt-umbraco-package');
	grunt.loadNpmTasks('grunt-contrib-clean');
	grunt.loadNpmTasks('grunt-contrib-copy');
	grunt.loadNpmTasks('grunt-nuget');
	grunt.loadNpmTasks('grunt-zip');

	grunt.registerTask('release', ['clean', 'copy', 'zip', 'umbracoPackage', 'nugetpack', 'clean']);

	grunt.registerTask('default', ['release']);

};